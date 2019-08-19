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
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public UsersController(
            IUserService userSrv,
            IAuthService authSrv,
            IMapper mapper)
        {
            _userService = userSrv;
            _authService = authSrv;
            _mapper = mapper;
        }


        [HttpGet("[action]")]
        public IActionResult SearchUsers([FromQuery]UsersFilterViewModel filter)
        {
            filter.PageSize = 4;
            try
            {
                var viewModel = new IndexViewModel<UserManageDto>
                {
                    Items = _mapper.Map<IEnumerable<UserManageDto>>(_userService.Get(filter, out int count)),
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize)
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        #region Users managment by Admin

        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromQuery]UsersFilterViewModel filter)
        {
            if (filter.PageSize == 0)
            {
                filter.PageSize = 10;
            }
            try
            {
                var viewModel = new IndexViewModel<UserManageDto>
                {
                    Items = _mapper.Map<IEnumerable<UserManageDto>>(_userService.Get(filter, out int count)),
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize)
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }


        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(Guid userId, Guid roleId)
        {
            var result = await _userService.ChangeRole(userId, roleId);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }


        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Unblock(Guid userId)
        {
            var result = await _userService.Unblock(userId);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Block(Guid userId)
        {
            var result = await _userService.Block(userId);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        #endregion

        #region My profile managment

        [HttpPost("[action]")]
        public async Task<IActionResult> EditUsername(UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.Name))
            {
                return BadRequest();
            }

            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            user.Name = userInfo.Name;
            var result = await _userService.Update(user);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditBirthday(UserInfo userInfo)
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

            user.Birthday = userInfo.Birthday;
            var result = await _userService.Update(user);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditGender(UserInfo userInfo)
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

            user.Gender = (Gender)userInfo.Gender;
            var result = await _userService.Update(user);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditUserCategory(UserInfo userInfo)
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

            var newCategories = _mapper.Map<IEnumerable<Category>>(userInfo.Categories);

            var result = await _userService.EditFavoriteCategories(user, newCategories);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeAvatar([FromForm]IFormFile newAva)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            newAva = HttpContext.Request.Form.Files[0];

            var result = await _userService.ChangeAvatar(user.Id, newAva);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            var updatedPhoto = _userService.GetById(user.Id).Photo.Thumb.ToRenderablePictureString();
            return Ok(updatedPhoto);
        }

        #endregion


        [HttpGet("[action]")]
        public IActionResult GetUserProfileById(Guid id)
        {
            var user = GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<ProfileDto>(_userService.GetProfileById(id, user.Id));

            return Ok(res);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> SetAttitude(AttitudeDto attitude)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.SetAttitude(_mapper.Map<AttitudeDTO>(attitude));
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }


        // HELPERS: 
        private UserDTO GetCurrentUser(ClaimsPrincipal userClaims) => _authService.GetCurrentUser(userClaims);


    }
}