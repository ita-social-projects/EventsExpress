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
    public class UserPhotoService : PhotoService, IUserPhotoService
    {
        private readonly IOptions<ImageOptionsModel> _widthOptions;

        public UserPhotoService(IOptions<ImageOptionsModel> opt, BlobServiceClient blobServiceClient)
            : base(blobServiceClient)
        {
            _widthOptions = opt;
        }

        public async Task AddUserPhoto(IFormFile uploadedFile, Guid id)
        {
            var photo = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail);

            await UploadPhotoToBlob(photo, $"users/{id}/photo.png");
        }

        public async Task DeleteUserPhoto(Guid id)
        {
            await DeletePhotoFromAzureBlob($"users/{id}/photo.png");
        }

        public async Task<byte[]> GetUserPhoto(Guid id)
        {
            return await GetPhotoFromAzureBlob($"users/{id}/photo.png");
        }
    }
}
