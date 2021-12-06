using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using FluentValidation;
using FreeImageAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EventsExpress.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly IOptions<ImageOptionsModel> _widthOptions;

        public PhotoService(
            IOptions<ImageOptionsModel> opt,
            BlobServiceClient blobServiceClient)
        {
            _widthOptions = opt;
            _blobContainerClient = blobServiceClient.GetBlobContainerClient("images");
        }

        public async Task ChangeTempToImagePhoto(Guid id)
        {
            byte[] photo = await GetPhotoFromAzureBlob($"events/{id}/previewTemp.png");
            await UploadPhotoToBlob(photo, $"events/{id}/preview.png");
            photo = await GetPhotoFromAzureBlob($"events/{id}/fullTemp.png");
            await UploadPhotoToBlob(photo, $"events/{id}/full.png");
        }

        public async Task AddUserPhoto(IFormFile uploadedFile, Guid id)
        {
            var photo = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail);

            await UploadPhotoToBlob(photo, $"users/{id}/photo.png");
        }

        public byte[] GetResizedBytesFromFile(IFormFile file, int newWidth)
        {
            using var memoryStream = file.ToMemoryStream();
            var oldBitMap = new FreeImageBitmap(memoryStream);

            int width = newWidth;
            int height = oldBitMap.Height * newWidth / oldBitMap.Width;

            var newBitmap = new FreeImageBitmap(oldBitMap, width, height);
            return ImageToByteArray(newBitmap);
        }

        private static byte[] ImageToByteArray(FreeImageBitmap imageIn)
        {
            using var ms = new MemoryStream();
            imageIn.Save(ms, FREE_IMAGE_FORMAT.FIF_PNG);
            return ms.ToArray();
        }

        private async Task UploadPhotoToBlob(byte[] photo, string url)
        {
            _blobContainerClient.CreateIfNotExists();

            if (photo != null)
            {
                using var stream = new MemoryStream(photo, false);
                BlobClient blobClient = _blobContainerClient.GetBlobClient(url);
                BlobUploadOptions blobUploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = "image/png",
                    },
                };
                await blobClient.UploadAsync(stream, blobUploadOptions, default);
            }
        }

        public async Task<byte[]> GetPhotoFromAzureBlob(string url)
        {
            try
            {
                using var previewMS = new MemoryStream();
                BlobClient blobClient = _blobContainerClient.GetBlobClient(url);
                await blobClient.DownloadToAsync(previewMS);
                return previewMS.ToArray();
            }
            catch (Azure.RequestFailedException)
            {
                return null;
            }
        }

        public async Task AddEventTempPhoto(IFormFile uploadedFile, Guid id)
        {
            var previewPhoto = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail);
            await UploadPhotoToBlob(previewPhoto, $"events/{id}/previewTemp.png");

            var fullPhoto = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Image);
            await UploadPhotoToBlob(fullPhoto, $"events/{id}/fullTemp.png");
        }
    }
}
