using System;
using System.IO;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.IServices
{
    public interface IPhotoService
    {
        Task AddEventTempPhoto(IFormFile uploadedFile, Guid id);

        Task AddUserPhoto(IFormFile uploadedFile, Guid id);

        Task ChangeTempToImagePhoto(Guid id);

        Task<byte[]> GetPhotoFromAzureBlob(string url);
    }
}
