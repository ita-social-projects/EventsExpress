using System;
using System.IO;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.IServices
{
    public interface IPhotoService
    {
        Task AddEventPhoto(IFormFile uploadedFile, Guid id);

        Task AddUserPhoto(IFormFile uploadedFile, Guid id);

        Task AddPhotoByURL(string url, Guid id);

        Task<byte[]> GetPhotoFromAzureBlob(string url);
    }
}
