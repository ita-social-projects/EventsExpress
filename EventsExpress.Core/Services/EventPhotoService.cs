using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EventsExpress.Core.Services
{
    public class EventPhotoService : PhotoService, IEventPhotoService
    {
        private readonly IOptions<EventImageOptionsModel> _widthOptions;

        public EventPhotoService(IOptions<EventImageOptionsModel> opt, BlobServiceClient blobServiceClient)
            : base(blobServiceClient)
        {
            _widthOptions = opt;
        }

        public async Task ChangeTempToImagePhoto(Guid id)
        {
            byte[] photo = await GetPhotoFromAzureBlob($"events/{id}/previewTemp.png");
            await UploadPhotoToBlob(photo, $"events/{id}/preview.png");
            photo = await GetPhotoFromAzureBlob($"events/{id}/fullTemp.png");
            await UploadPhotoToBlob(photo, $"events/{id}/full.png");
        }

        public async Task AddEventTempPhoto(IFormFile uploadedFile, Guid id)
        {
            var previewPhoto = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail);
            await UploadPhotoToBlob(previewPhoto, $"events/{id}/previewTemp.png");

            var fullPhoto = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Image);
            await UploadPhotoToBlob(fullPhoto, $"events/{id}/fullTemp.png");
        }

        public async Task<byte[]> GetPreviewEventPhoto(Guid id)
        {
            return await GetPhotoFromAzureBlob($"events/{id}/preview.png");
        }

        public async Task<byte[]> GetFullEventPhoto(Guid id)
        {
            return await GetPhotoFromAzureBlob($"events/{id}/full.png");
        }
    }
}
