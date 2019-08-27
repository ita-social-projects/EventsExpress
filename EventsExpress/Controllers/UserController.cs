using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.DTO;
using EventsExpress.Validation;
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

        /// <summary>
        /// This method
        /// </summary>
        /// <param name="filter">Required</param>
        /// <returns></returns>
        /// <response code="200">Return IEnumerable UserManageDto models</response>
        /// <response code="400">Return failed</response>
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

        /// <summary>
        /// This metod have to return UserMangeDto
        /// </summary>
        /// <param name="filter">Required</param>
        /// <returns></returns>
        /// <response code="200">Return  UserManageDto model</response>
        /// <response code="400">Return failed</response>
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

        /// <summary>
        /// This method have to change role of user
        /// </summary>
        /// <param name="userId">Required</param>
        /// <param name="roleId">Required</param>
        /// <returns></returns>
        /// <response code="200">Change role success</response>
        /// <response code="400">Change role failed</response>
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

        /// <summary>
        /// This method is to block user
        /// </summary>
        /// <param name="userId">Required</param>
        /// <returns></returns>
        /// <response code="200">Block is succesful</response>
        /// <response code="400">Block process failed</response>
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

        /// <summary>
        /// This method is to unblock event
        /// </summary>
        /// <param name="userId">Required</param>
        /// <returns></returns>
        /// <response code="200">Unblock is succesful</response>
        /// <response code="400">Unblock process failed</response>
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
            var validator = new UserInfoAgeValidator();
            
            var validationResult = validator.Validate(userInfo);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
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

        /// <summary>
        /// This method is for get user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return profileDto</response>
        
        [HttpGet("[action]")]
        public IActionResult GetUserProfileById(Guid id)
        {
            var user = GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<ProfileDto>(_userService.GetProfileById(id, user.Id));

            return Ok(res);
        }

        /// <summary>
        /// This method is to set attitide t user
        /// </summary>
        /// <param name="attitude"></param>
        /// <returns></returns>
        /// <response code="200">Attitude set success</response>    
        /// <response code="400">Attitude set failed</response>
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