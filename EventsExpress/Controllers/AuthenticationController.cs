using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Helpers;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthServicre _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IUserService userSrv,
            IMapper mapper,
            IAuthServicre authSrv
            )
        {
            _userService = userSrv;
            _mapper = mapper;
            _authService = authSrv;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Login(LoginDto authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _authService.Authenticate(authRequest.Email, authRequest.Password);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            var user = _userService.GetByEmail(authRequest.Email);

            var userInfo = _mapper.Map<UserDTO, UserInfo>(user);
            userInfo.Token = result.Message;

            return Ok(userInfo);
        }


        [Authorize]
        [HttpPost("login_token")]
        public IActionResult Login()
        {
            var user = _authService.GetCurrentUser(HttpContext.User);

            return Ok(_mapper.Map<UserDTO, UserInfo>(user));
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(LoginDto authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<LoginDto, UserDTO> (authRequest);
            user.PasswordHash = PasswordHasher.GenerateHash(authRequest.Password);

            var result = await _userService.Create(user);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> PasswordRecovery(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            var user = _userService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest("User with this email is not found");
            }

            var result = await _userService.PasswordRecover(user);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }


        [AllowAnonymous]
        [HttpPost("verify/{userid}/{token}")]
        public async Task<IActionResult> EmailConfirm(string userid, string token)
        {
            var cache = new CacheDTO { Token = token };

            if (!Guid.TryParse(userid, out cache.UserId))
            {
                return BadRequest();
            }

            var result = await _userService.Verificate(cache);

            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            var user = _userService.GetById(cache.UserId);

            var userInfo = _mapper.Map<UserDTO, UserInfo>(user);
            userInfo.Token = _authService.FirstAuthenticate(user).Message;
            userInfo.AfterEmailConfirmation = true;

            return Ok(userInfo);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = _authService.GetCurrentUser(HttpContext.User);

            var result = await _authService.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }
       
    }
}