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
    public abstract class PhotoService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public PhotoService(BlobServiceClient blobServiceClient)
        {
            _blobContainerClient = blobServiceClient.GetBlobContainerClient("images");
        }

        protected static byte[] ImageToByteArray(FreeImageBitmap imageIn)
        {
            using var ms = new MemoryStream();
            imageIn.Save(ms, FREE_IMAGE_FORMAT.FIF_PNG);
            return ms.ToArray();
        }

        protected static byte[] GetResizedBytesFromFile(IFormFile file, int newWidth)
        {
            using var memoryStream = file.ToMemoryStream();
            var oldBitMap = new FreeImageBitmap(memoryStream);

            int width = newWidth;
            int height = oldBitMap.Height * newWidth / oldBitMap.Width;

            var newBitmap = new FreeImageBitmap(oldBitMap, width, height);
            return ImageToByteArray(newBitmap);
        }

        protected static byte[] GetBytesFromFile(IFormFile file)
        {
            using var memoryStream = file.ToMemoryStream();
            var bitMap = new FreeImageBitmap(memoryStream);
            return ImageToByteArray(bitMap);
        }

        protected async Task UploadPhotoToBlob(byte[] photo, string url)
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

        protected async Task<byte[]> GetPhotoFromAzureBlob(string url)
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

        protected async Task DeletePhotoFromAzureBlob(string url)
        {
            try
            {
                await _blobContainerClient.DeleteBlobAsync(url);
            }
            catch (Azure.RequestFailedException)
            {
                throw;
            }
        }
    }
}
