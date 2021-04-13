using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPhotoService _photoService;

        public UsersController(
            IUserService userSrv,
            IAuthService authSrv,
            IMapper mapper,
            IEmailService emailService,
            IPhotoService photoService)
        {
            _userService = userSrv;
            _authService = authSrv;
            _mapper = mapper;
            _emailService = emailService;
            _photoService = photoService;
        }

        /// <summary>
        /// This method seach Users with filter.
        /// </summary>
        /// <param name="filter">Param filter defines UsersFilterViewModel.</param>
        /// <returns>The method returns found user.</returns>
        /// <response code="200">Return IEnumerable UserManageDto models.</response>
        /// <response code="400">Return failed.</response>
        [HttpGet("[action]")]
        [Authorize(Policy = "UserPolicy")]
        public IActionResult SearchUsers([FromQuery] UsersFilterViewModel filter)
        {
            filter.PageSize = 12;
            filter.IsConfirmed = true;
            try
            {
                var user = GetCurrentUser(HttpContext.User);
                var viewModel = new IndexViewModel<UserManageViewModel>
                {
                    Items = _mapper.Map<IEnumerable<UserManageViewModel>>(_userService.Get(filter, out int count, user.Id)),
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize),
                };

                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This metod have to return UserDto for Admin.
        /// </summary>
        /// <param name="filter">Param filter defines UsersFilterViewModel.</param>
        /// <returns>The method returns all users.</returns>
        /// <response code="200">Return  UserManageDto model.</response>
        /// <response code="400">Return failed.</response>
        [HttpGet("[action]")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Get([FromQuery] UsersFilterViewModel filter)
        {
            if (filter.PageSize == 0)
            {
                filter.PageSize = 10;
            }

            try
            {
                var user = GetCurrentUser(HttpContext.User);
                var viewModel = new IndexViewModel<UserManageViewModel>
                {
                    Items = _mapper.Map<IEnumerable<UserDto>, IEnumerable<UserManageViewModel>>(_userService.Get(filter, out int count, user.Id)),
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize),
                };

                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method have to change role of user.
        /// </summary>
        /// <param name="userId">Param userId defines the user identifier.</param>
        /// <param name="roleId">Param roleId defines the role identifier.</param>
        /// <returns>The method changes role for users.</returns>
        /// <response code="200">Change role success.</response>
        /// <response code="400">Change role failed.</response>
        [HttpPost("[action]")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ChangeRole(Guid userId, Guid roleId)
        {
            await _userService.ChangeRole(userId, roleId);

            return Ok();
        }

        /// <summary>
        /// This method is to block user.
        /// </summary>
        /// <param name="userId">Param userId defines the user identifier.</param>
        /// <returns>The method returns unblocked user.</returns>
        /// <response code="200">Block is succesful.</response>
        /// <response code="400">Block process failed.</response>
        [HttpPost("{userId}/[action]")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Unblock(Guid userId)
        {
            await _userService.Unblock(userId);

            return Ok();
        }

        /// <summary>
        /// This method is to unblock event.
        /// </summary>
        /// <param name="userId">Param userId defines the user identifier.</param>
        /// <returns>The method returns blocked user.</returns>
        /// <response code="200">Unblock is succesful.</response>
        /// <response code="400">Unblock process failed.</response>
        [HttpPost("[action]")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Block(Guid userId)
        {
            await _userService.Block(userId);

            return Ok();
        }

        /// <summary>
        /// This method is to edit username.
        /// </summary>
        /// <param name="userName">Param userName defines the username.</param>
        /// <returns>The method returns edited username.</returns>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditUsername(EditUserNameViewModel userName)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            user.Name = userName.Name;
            await _userService.Update(user);

            return Ok();
        }

        /// <summary>
        /// This method is to edit date of birthday.
        /// </summary>
        /// <param name="userBirthday">Param userBirthday defines the user Birthday.</param>
        /// <returns>The method returns edited birthday.</returns>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditBirthday(EditUserBirthViewModel userBirthday)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            user.Birthday = userBirthday.Birthday;
            await _userService.Update(user);

            return Ok();
        }

        /// <summary>
        /// This method is to edit gender.
        /// </summary>
        /// <param name="userGender">Param userGender defines the user gender.</param>
        /// <returns>The method returns edited gender.</returns>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditGender(EditUserGenderViewModel userGender)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            user.Gender = (Gender)userGender.Gender;
            await _userService.Update(user);

            return Ok();
        }

        /// <summary>
        /// This method is to edit user categories.
        /// </summary>
        /// <param name="model">Param model defines EditUserCategoriesViewModel model.</param>
        /// <returns>The method returns edited categories for user.</returns>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditUserCategory(EditUserCategoriesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            var newCategories = _mapper.Map<IEnumerable<Category>>(model.Categories);

            await _userService.EditFavoriteCategories(user, newCategories);

            return Ok();
        }

        /// <summary>
        /// This metod is to change user avatar.
        /// </summary>
        /// <returns>The method returns edited profile photo.</returns>
        /// <response code="200">Changing is succesful.</response>
        /// <response code="400">Changing process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeAvatar()
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            var newAva = HttpContext.Request.Form.Files[0];

            await _userService.ChangeAvatar(user.Id, newAva);

            var updatedPhoto = _photoService.GetPhotoFromAzureBlob($"users/{user.Id}/photo.png").Result;

            return Ok(updatedPhoto);
        }

        /// <summary>
        /// This method help to contact users with admins.
        /// </summary>
        /// <param name="model">Param model defines ContactUsViewModel model.</param>
        /// <returns>The method sends message to admin mail.</returns>
        /// <response code="200">Sending is succesfull.</response>
        /// <response code="400">Sending process failed.</response>
        [HttpPost("[action]")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> ContactAdmins(ContactUsViewModel model)
        {
            var user = _authService.GetCurrentUser(HttpContext.User);

            var admins = _userService.GetUsersByRole("Admin");

            var emailBody = $"New request from <a href='mailto:{user.Email}?subject=re:{model.Type}'>{user.Email}</a> : <br />{model.Description}. ";

            try
            {
                foreach (var admin in admins)
                {
                    await _emailService.SendEmailAsync(new EmailDto
                    {
                        Subject = model.Type,
                        RecepientEmail = admin.Email,
                        MessageText = emailBody,
                    });
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method is for get user.
        /// </summary>
        /// <param name="id">Param id defines the user identifier.</param>
        /// <returns>The method returns user profile.</returns>
        /// <response code="200">Return profileDto.</response>
        /// <response code="400">Attitude set failed.</response>
        [HttpGet("[action]")]
        [Authorize(Policy = "UserPolicy")]
        public IActionResult GetUserProfileById(Guid id)
        {
            var user = GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<ProfileViewModel>(_userService.GetProfileById(id, user.Id));

            return Ok(res);
        }

        /// <summary>
        /// This method is to set attitide t user.
        /// </summary>
        /// <param name="attitude">Param attitude defines the attitude.</param>
        /// <returns>The method returns the specified attitude.</returns>
        /// <response code="200">Attitude set success.</response>
        /// <response code="400">Attitude set failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> SetAttitude(AttitudeViewModel attitude)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.SetAttitude(_mapper.Map<AttitudeDto>(attitude));

            return Ok();
        }

        // HELPERS:

        /// <summary>
        /// This method help to get current user from JWT.
        /// </summary>
        /// <returns>The method returns current user.</returns>
        [NonAction]
        private UserDto GetCurrentUser(ClaimsPrincipal userClaims) => _authService.GetCurrentUser(userClaims);

        /// <summary>
        /// This method is to edit user notificatin types.
        /// </summary>
        /// <param name="model">Param model defines EditUserNotificationTypesViewModel model.</param>
        /// <returns>The method returns edited notification types for user.</returns>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditUserNotificationType(EditUserNotificationTypesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                throw new EventsExpressException("Null object");
            }

            var newNotificationTypes = _mapper.Map<IEnumerable<NotificationType>>(model.NotificationTypes);

            var result = await _userService.EditFavoriteNotificationTypes(user, newNotificationTypes);

            return Ok(result);
        }
    }
}
