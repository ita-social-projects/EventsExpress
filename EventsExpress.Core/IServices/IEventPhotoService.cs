using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.IServices
{
    public interface IEventPhotoService
    {
        Task AddEventTempPhoto(IFormFile uploadedFile, Guid id);

        Task ChangeTempToImagePhoto(Guid id);

        Task<byte[]> GetPreviewEventPhoto(Guid id);

        Task<byte[]> GetFullEventPhoto(Guid id);
    }
}
