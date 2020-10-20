using System;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.IServices
{
    public interface IPhotoService
    {
        Task<Photo> AddPhoto(IFormFile uploadedFile);

        Task Delete(Guid id);
    }
}
