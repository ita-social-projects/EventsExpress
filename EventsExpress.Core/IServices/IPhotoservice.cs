using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IPhotoService
    {
        Task<Photo> AddPhoto(IFormFile uploadedFile);
        Task Delete(Guid id);
    }
}
