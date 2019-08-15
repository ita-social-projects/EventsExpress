using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
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
        private readonly IMapper _mapper;
     
        public UsersController(IUserService userSrv, IMapper mapper)
        {         
            _userService = userSrv;
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
                    Items = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserManageDto>>(_userService.Get(filter, out int count)),
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
                    Items = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserManageDto>>(_userService.Get(filter, out int count)),
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
            var user = _userService.GetByEmail(HttpContext.User.Claims?.First().Value);
            if (user == null)
            {
                return BadRequest();
            }

            string newName = userInfo.Name;
            if (string.IsNullOrEmpty(newName))
            {
                return BadRequest();
            } 

            user.Name = newName;

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
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            DateTime newBirthday = userInfo.Birthday;
            user.Birthday = newBirthday;

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
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            byte newGender = userInfo.Gender;
            user.Gender = (Gender)newGender;

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
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            IEnumerable<Category> newCategories = _mapper.Map<IEnumerable<CategoryDto>, IEnumerable<Category>>(userInfo.Categories);

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

            var updatedPhoto = _userService.GetById(user.Id).Photo.Thumb.ToRenderablePictureString();
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            return Ok(updatedPhoto);
        }

        #endregion


        [HttpGet("[action]")]
        public IActionResult GetUserById(Guid id)
        {
            var user = GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<ProfileDTO, ProfileDto>(_userService.GetProfileById(id, user.Id));

            return Ok(res);
        }

        
        [HttpGet("[action]")]
        public IActionResult GetAttitude(AttitudeDto attitude)
        {
            var res = _mapper.Map<AttitudeDTO, AttitudeDto>(_userService.GetAttitude(_mapper.Map<AttitudeDto, AttitudeDTO>(attitude)));
            return Ok(res);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> SetAttitude(AttitudeDto attitude)
        {
            var result = await _userService.SetAttitude(_mapper.Map<AttitudeDto, AttitudeDTO>(attitude)); 
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        // HELPERS: 

        private UserDTO GetCurrentUser(ClaimsPrincipal userClaims)
        {
            string email = userClaims.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }
            return _userService.GetByEmail(email);
        }

    }
}