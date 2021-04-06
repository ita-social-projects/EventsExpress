using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

namespace EventsExpress.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly IOptions<ImageOptionsModel> _widthOptions;
        private readonly Lazy<HttpClient> _client;

        public PhotoService(
            IOptions<ImageOptionsModel> opt,
            IHttpClientFactory clientFactory,
            BlobServiceClient blobServiceClient)
        {
            _widthOptions = opt;
            _client = new Lazy<HttpClient>(() => clientFactory.CreateClient());
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

        public async Task AddUserPhoto(IFormFile uploadedFile, Guid id)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException("The upload file should be a valid image", nameof(uploadedFile));
            }

            var photo = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail);

            await UploadPhotoToBlob(photo, $"users/{id}/photo.png");
        }

        public async Task AddPhotoByURL(string url, Guid id)
        {
            if (!await IsImageUrl(url))
            {
                throw new ArgumentException("The url should be a valid image", nameof(url));
            }

            Uri uri = new Uri(url);
            byte[] photo = _client.Value.GetByteArrayAsync(uri).Result;

            await UploadPhotoToBlob(photo, $"users/{id}/photo.png");
        }

        private async Task<bool> IsImageUrl(string url)
        {
            try
            {
                HttpResponseMessage result = await _client.Value.GetAsync(url);
                return result.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
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

        public async Task<string> GetPhotoFromAzureBlob(string url)
        {
            try
            {
                using var previewMS = new MemoryStream();
                BlobClient blobClient = _blobContainerClient.GetBlobClient(url);
                await blobClient.DownloadToAsync(previewMS);
                return previewMS.ToArray().ToRenderablePictureString();
            }
            catch (Azure.RequestFailedException)
            {
                return null;
            }
        }
    }
}
