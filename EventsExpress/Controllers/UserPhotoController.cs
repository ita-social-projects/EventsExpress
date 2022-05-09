using System;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    public class UserPhotoController : Controller
    {
        private readonly IUserPhotoService _userPhotoService;
        private readonly IUserService _userService;

        public UserPhotoController(IUserPhotoService userPhotoService, IUserService userService)
        {
            _userPhotoService = userPhotoService;
            _userService = userService;
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

        /// <summary>
        /// This method is to change user avatar.
        /// </summary>
        /// <param name="userId">Param id defines the user identifier.</param>
        /// <param name="avatar">Param avatar defines the PhotoViewModel model.</param>
        /// <returns>The method returns edited profile photo.</returns>
        /// <response code="200">Changing is successful.</response>
        /// <response code="400">Changing process failed.</response>
        [HttpPost("[action]/{userId:Guid}")]
        public async Task<IActionResult> ChangeAvatar(Guid userId, [FromForm] UserPhotoViewModel avatar)
        {
            if (ModelState.IsValid)
            {
                await _userService.ChangeAvatar(avatar.Photo);

                var updatedPhoto = await _userPhotoService.GetUserPhoto(userId);

                return Ok(updatedPhoto);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
