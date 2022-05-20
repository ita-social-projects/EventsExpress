using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EventsExpress.Core.Services
{
    public class UserPhotoService : PhotoService, IUserPhotoService
    {
        public UserPhotoService(BlobServiceClient blobServiceClient)
            : base(blobServiceClient)
        {
        }

        public async Task AddUserPhoto(IFormFile uploadedFile, Guid id)
        {
            var photo = GetBytesFromFile(uploadedFile);

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
