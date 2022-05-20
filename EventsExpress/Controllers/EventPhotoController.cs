using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventPhotoController : ControllerBase
    {
        private readonly IEventPhotoService _eventPhotoService;

        public EventPhotoController(IEventPhotoService eventPhotoService)
        {
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

        [HttpPost("[action]/{eventId:Guid}")]
        public async Task<IActionResult> SetEventTempPhoto(Guid eventId, [FromForm] EventPhotoViewModel photo)
        {
            await _eventPhotoService.AddEventTempPhoto(photo.Photo, eventId);

            return Ok();
        }
    }
}
