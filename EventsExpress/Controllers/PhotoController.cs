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
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPreviewEventPhoto(string id)
        {
            string url = $"events/{id}/preview.png";

            var photo = await _photoService.GetPhotoFromAzureBlob(url);

            if (photo == null)
            {
                return NotFound();
            }

            return File(photo, "image/png");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFullEventPhoto(string id)
        {
            string url = $"events/{id}/full.png";

            var photo = await _photoService.GetPhotoFromAzureBlob(url);

            if (photo == null)
            {
                return NotFound();
            }

            return File(photo, "image/png");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserPhoto(string id)
        {
            string url = $"users/{id}/photo.png";

            var photo = await _photoService.GetPhotoFromAzureBlob(url);

            if (photo == null)
            {
                return NotFound();
            }

            return File(photo, "image/png");
        }
    }
}
