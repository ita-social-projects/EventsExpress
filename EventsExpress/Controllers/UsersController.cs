using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.DTO;
using EventsExpress.ViewModel;
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

        public UsersController(
            IUserService userSrv,
            IAuthService authSrv,
            IMapper mapper,
            IEmailService emailService)
        {
            _userService = userSrv;
            _authService = authSrv;
            _mapper = mapper;
            _emailService = emailService;
        }

        /// <summary>
        /// This method seach Users with filter.
        /// </summary>
        /// <param name="filter">Required.</param>
        /// <returns>Users.</returns>
        /// <response code="200">Return IEnumerable UserManageDto models.</response>
        /// <response code="400">Return failed.</response>
        [HttpGet("[action]")]
        public IActionResult SearchUsers([FromQuery] UsersFilterViewModel filter)
        {
            filter.PageSize = 12;
            filter.IsConfirmed = true;
            try
            {
                var user = GetCurrentUser(HttpContext.User);
                var viewModel = new IndexViewModel<UserManageDto>
                {
                    Items = _mapper.Map<IEnumerable<UserManageDto>>(_userService.Get(filter, out int count, user.Id)),
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
        /// <param name="filter">Required.</param>
        /// <returns>Users.</returns>
        /// <response code="200">Return  UserManageDto model.</response>
        /// <response code="400">Return failed.</response>
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromQuery] UsersFilterViewModel filter)
        {
            if (filter.PageSize == 0)
            {
                filter.PageSize = 10;
            }

            try
            {
                var user = GetCurrentUser(HttpContext.User);
                var viewModel = new IndexViewModel<UserManageDto>
                {
                    Items = _mapper.Map<IEnumerable<UserManageDto>>(_userService.Get(filter, out int count, user.Id)),
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
        /// <param name="userId">Required.</param>
        /// <param name="roleId">UserRoleId.</param>
        /// <response code="200">Change role success.</response>
        /// <response code="400">Change role failed.</response>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(Guid userId, Guid roleId)
        {
            await _userService.ChangeRole(userId, roleId);

            return Ok();
        }

        /// <summary>
        /// This method is to block user.
        /// </summary>
        /// <param name="userId">Required.</param>
        /// <response code="200">Block is succesful.</response>
        /// <response code="400">Block process failed.</response>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Unblock(Guid userId)
        {
            await _userService.Unblock(userId);

            return Ok();
        }

        /// <summary>
        /// This method is to unblock event.
        /// </summary>
        /// <param name="userId">Required.</param>
        /// <response code="200">Unblock is succesful.</response>
        /// <response code="400">Unblock process failed.</response>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Block(Guid userId)
        {
            await _userService.Block(userId);

            return Ok();
        }

        /// <summary>
        /// This method is to edit username.
        /// </summary>
        /// <param name="userName">Required.</param>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditUsername(EditUserNameDto userName)
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
        /// <param name="userBirthday">Required.</param>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditBirthday(EditUserBirthDto userBirthday)
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
        /// <param name="userGender">Required.</param>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditGender(EditUserGenderDto userGender)
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
        /// <param name="model">Required.</param>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditUserCategory(EditUserCategoriesDto model)
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

            var updatedPhoto = _userService.GetById(user.Id).Photo.Thumb.ToRenderablePictureString();

            return Ok(updatedPhoto);
        }

        /// <summary>
        /// This method help to contact users with admins.
        /// </summary>
        /// <param name="model">ContactModel.</param>
        /// <response code="200">Sending is succesfull.</response>
        /// <response code="400">Sending process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> ContactAdmins(ContactUsDto model)
        {
            var user = _authService.GetCurrentUser(HttpContext.User);

            var admins = _userService.GetUsersByRole("Admin");

            var emailBody = $"New request from <a href='mailto:{user.Email}?subject=re:{model.Type}'>{user.Email}</a> : <br />{model.Description}. ";

            try
            {
                foreach (var admin in admins)
                {
                    await _emailService.SendEmailAsync(new EmailDTO
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
        /// <param name="id">UserId.</param>
        /// <returns>User.</returns>
        /// <response code="200">Return profileDto.</response>
        /// <response code="400">Attitude set failed.</response>
        [HttpGet("[action]")]
        public IActionResult GetUserProfileById(Guid id)
        {
            var user = GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<ProfileDto>(_userService.GetProfileById(id, user.Id));

            return Ok(res);
        }

        /// <summary>
        /// This method is to set attitide t user.
        /// </summary>
        /// <response code="200">Attitude set success.</response>
        /// <response code="400">Attitude set failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> SetAttitude(AttitudeDto attitude)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.SetAttitude(_mapper.Map<AttitudeDTO>(attitude));

            return Ok();
        }

        // HELPERS:

        /// <summary>
        /// This method help to get current user from JWT.
        /// </summary>
        [NonAction]
        private UserDTO GetCurrentUser(ClaimsPrincipal userClaims) => _authService.GetCurrentUser(userClaims);
    }
}
