using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
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

        private static bool IsValidImage(IFormFile file) => file != null && file.IsImage();

        public async Task AddEventPhoto(IFormFile uploadedFile, Guid id)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException("The upload file should be a valid image", nameof(uploadedFile));
            }

            var previewPhoto = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail);
            await UploadPhotoToBlob(previewPhoto, $"events/{id}/preview.png");

            var fullPhoto = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Image);
            await UploadPhotoToBlob(fullPhoto, $"events/{id}/full.png");
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
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException("The upload file should be a valid image", nameof(uploadedFile));
            }

            var photo = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail);

            await UploadPhotoToBlob(photo, $"users/{id}/photo.png");
        }

        public byte[] GetResizedBytesFromFile(IFormFile file, int newWidth)
        {
            using var memoryStream = file.ToMemoryStream();
            var oldBitMap = new Bitmap(memoryStream);
            var newSize = new Size
            {
                Width = newWidth,
                Height = oldBitMap.Size.Height * newWidth / oldBitMap.Size.Width,
            };

            var newBitmap = new Bitmap(oldBitMap, newSize);

            return ImageToByteArray(newBitmap);
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);

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
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException("The upload file should be a valid image", nameof(uploadedFile));
            }

            var previewPhoto = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail);
            await UploadPhotoToBlob(previewPhoto, $"events/{id}/previewTemp.png");

            var fullPhoto = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Image);
            await UploadPhotoToBlob(fullPhoto, $"events/{id}/fullTemp.png");
        }
    }
}
