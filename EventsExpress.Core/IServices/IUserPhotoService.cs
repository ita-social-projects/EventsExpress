using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.IServices
{
    public interface IUserPhotoService
    {
        Task AddUserPhoto(IFormFile uploadedFile, Guid id);

        Task DeleteUserPhoto(Guid id);

        Task<byte[]> GetUserPhoto(Guid id);
    }
}
