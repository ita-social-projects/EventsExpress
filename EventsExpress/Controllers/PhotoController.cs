using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IUserPhotoService _userPhotoService;
        private readonly IEventPhotoService _eventPhotoService;

        public PhotoController(IUserPhotoService userPhotoService, IEventPhotoService eventPhotoService)
        {
            _userPhotoService = userPhotoService;
            _eventPhotoService = eventPhotoService;
        }

        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetPreviewEventPhoto(Guid id)
        {
            var photo = await _eventPhotoService.GetPreviewEventPhoto(id);

            if (photo == null)
            {
                return NotFound();
            }

            return File(photo, "image/png");
        }

        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetFullEventPhoto(Guid id)
        {
            var photo = await _eventPhotoService.GetFullEventPhoto(id);

            if (photo == null)
            {
                return NotFound();
            }

            return File(photo, "image/png");
        }

        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetUserPhoto(Guid id)
        {
            var photo = await _userPhotoService.GetUserPhoto(id);

            if (photo == null)
            {
                return NotFound();
            }

            return File(photo, "image/png");
        }

        [HttpPost("[action]/{id:Guid}")]
        public async Task<IActionResult> DeleteUserPhoto(Guid id)
        {
            await _userPhotoService.DeleteUserPhoto(id);

            return Ok();
        }
    }
}
