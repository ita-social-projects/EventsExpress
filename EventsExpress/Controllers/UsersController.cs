using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Policies;
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
        private readonly ISecurityContext _securityContext;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(
            IUserService userSrv,
            IMapper mapper,
            IPhotoService photoService,
            ISecurityContext securityContext)
        {
            _userService = userSrv;
            _photoService = photoService;
            _mapper = mapper;
            _securityContext = securityContext;
        }

        /// <summary>
        /// This method seach Users with filter.
        /// </summary>
        /// <param name="filter">Param filter defines UsersFilterViewModel.</param>
        /// <returns>The method returns found user.</returns>
        /// <response code="200">Return IEnumerable UserManageDto models.</response>
        /// <response code="400">Return failed.</response>
        [HttpGet("[action]")]
        [Authorize(Policy = PolicyNames.UserPolicyName)]
        public IActionResult SearchUsers([FromQuery] UsersFilterViewModel filter)
        {
            filter.PageSize = 12;
            filter.IsConfirmed = true;
            try
            {
                var viewModel = new IndexViewModel<UserManageViewModel>
                {
                    Items = _mapper.Map<IEnumerable<UserManageViewModel>>(_userService.Get(filter, out int count)),
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
        [Authorize(Policy = PolicyNames.AdminPolicyName)]
        public IActionResult Get([FromQuery] UsersFilterViewModel filter)
        {
            if (filter.PageSize == 0)
            {
                filter.PageSize = 10;
            }

            try
            {
                var viewModel = new IndexViewModel<UserManageViewModel>
                {
                    Items = _mapper.Map<IEnumerable<UserDto>, IEnumerable<UserManageViewModel>>(_userService.Get(filter, out int count)),
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize),
                };

                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetUserInfo()
        {
            var user = GetCurrentUserOrNull();
            var userInfo = _mapper.Map<UserDto, UserInfoViewModel>(user);

            return Ok(userInfo);
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
            await _userService.EditUserName(userName.Name);

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
            await _userService.EditBirthday(userBirthday.Birthday);

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
            await _userService.EditGender((Gender)userGender.Gender);

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

            var newCategories = _mapper.Map<IEnumerable<Category>>(model.Categories);

            await _userService.EditFavoriteCategories(newCategories);

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
            var userId = _securityContext.GetCurrentUserId();

            var newAva = HttpContext.Request.Form.Files[0];

            await _userService.ChangeAvatar(userId, newAva);

            var updatedPhoto = _photoService.GetPhotoFromAzureBlob($"users/{userId}/photo.png").Result;

            return Ok(updatedPhoto);
        }

        /// <summary>
        /// This method is for get user.
        /// </summary>
        /// <param name="id">Param id defines the user identifier.</param>
        /// <returns>The method returns user profile.</returns>
        /// <response code="200">Return profileDto.</response>
        /// <response code="400">Attitude set failed.</response>
        [HttpGet("[action]")]
        [Authorize(Policy = PolicyNames.UserPolicyName)]
        public IActionResult GetUserProfileById(Guid id)
        {
            var res = _mapper.Map<ProfileViewModel>(_userService.GetProfileById(id));

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

            var newNotificationTypes = _mapper.Map<IEnumerable<NotificationType>>(model.NotificationTypes);

            var result = await _userService.EditFavoriteNotificationTypes(newNotificationTypes);

            return Ok(result);
        }

        [NonAction]
        private UserDto GetCurrentUserInfo() => _userService.GetCurrentUserInfo();

        [NonAction]
        private UserDto GetCurrentUserOrNull()
        {
            try
            {
                return GetCurrentUserInfo();
            }
            catch (EventsExpressException)
            {
                return null;
            }
        }
    }
}
