using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
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
        public IActionResult All()
        {
            var users = _userService.GetAll();
            
            return Ok(_mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserPreviewDto>>(users));
        }

        [HttpGet("blocked")]
        public IActionResult GetBlockedUsers()
        {
            var users = _userService.Get(u => u.IsBlocked);

            return Ok(users);
        }

        [HttpPost("[action]")]
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
        [AllowAnonymous]
        public async Task<IActionResult> ChangeAvatar(
            //Guid userId, 
            IFormFile newAva)
        {
            //var result = await _userService.ChangeAvatar(userId, newAva);
            var result = await _userService.ChangeAvatar(new Guid("a27514cd-a007-44f6-a0fe-08d7039626cc"), newAva);

            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }
    }
}