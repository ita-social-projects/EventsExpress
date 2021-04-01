using System;
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

        Task<string> GetPhotoFromAzureBlob(string url);
    }
}
