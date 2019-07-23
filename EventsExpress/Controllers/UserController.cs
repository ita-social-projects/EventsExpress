using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.DTO;
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
        private IUserService _userService;
        private IMapper _mapper;

        public UsersController(IUserService userSrv, IMapper mapper)
        {
            _userService = userSrv;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        [HttpGet("blocked")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetBlockedUsers()
        {
            var users = _userService.Get(u => u.IsBlocked);

            return Ok(users);
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
        public async Task<IActionResult> EditUsername(UserInfo userInfo)
        {
            string newName = userInfo.Name;
            if (string.IsNullOrEmpty(newName))
            {
                return BadRequest();
            } 

            var user = _userService.GetByEmail(HttpContext.User.Claims?.First().Value);

            if (user == null)
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
            DateTime newBirthday = userInfo.Birthday;
           

            var user = _userService.GetByEmail(HttpContext.User.Claims?.First().Value);

            if (user == null)
            {
                return BadRequest();

            }

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
            byte newGender = userInfo.Gender;
            

            var user = _userService.GetByEmail(HttpContext.User.Claims?.First().Value);

            if (user == null)
            {
                return BadRequest();

            }

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
            IEnumerable<Category> newCategories = _mapper.Map<IEnumerable<CategoryDto>, IEnumerable<Category>>(userInfo.Categories);

            var user = _userService.GetByEmail(HttpContext.User.Claims?.First().Value);

            if (user == null)
            {
                return BadRequest();

            }

            var result = await _userService.EditFavoriteCategories(user, newCategories);

            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest();

        }
    }
}