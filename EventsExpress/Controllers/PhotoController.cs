using System.Threading.Tasks;
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
            string url = $"/events/{id}/preview.png";

            await _photoService.GetPhotoFromAzureBlob(url);

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFullEventPhoto(string id)
        {
            string url = $"/events/{id}/full.png";

            await _photoService.GetPhotoFromAzureBlob(url);

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserPhoto(string id)
        {
            string url = $"/users/{id}/preview.png";

            await _photoService.GetPhotoFromAzureBlob(url);

            return Ok();
        }
    }
}
