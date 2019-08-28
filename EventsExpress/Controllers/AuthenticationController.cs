using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Helpers;
using EventsExpress.DTO;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IUserService userSrv,
            IMapper mapper,
            IAuthService authSrv
            )
        {
            _userService = userSrv;
            _mapper = mapper;
            _authService = authSrv;
        }


        /// <summary>
        /// This method allows to log in to the API and generate an authentication token.
        /// </summary>
        /// <param name="authRequest">Required</param>
        /// <returns>UserInfo model</returns>
        /// <response code="200">Return UserInfo model</response>
        /// <response code="400">If login process failed</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
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

            var userInfo = _mapper.Map<UserInfo>(user);
            userInfo.Token = result.Message;

            userInfo.Rating = _userService.GetRating(userInfo.Id);

            return Ok(userInfo);
        }


        [AllowAnonymous]
        [HttpPost("FacebookLogin")]
        public async Task<IActionResult> FacebookLogin(UserView userView)
        {
            var userExisting = _userService.GetByEmail(userView.Email);
            if (userExisting == null)
            {
                var user = _mapper.Map<UserDTO>(userView);
                user.EmailConfirmed = true;
                await _userService.Create(user);
            }
            var auth = _authService.AuthenticateGoogleFacebookUser(userView.Email);
            if (!auth.Successed)
            {
                return BadRequest(auth.Message);
            }
            var userInfo = _mapper.Map<UserInfo>(_userService.GetByEmail(userView.Email));
            userInfo.Token = auth.Message;
            userInfo.Rating = _userService.GetRating(userInfo.Id);
            return Ok(userInfo);
        }


        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody]UserView userView)
        {
            var payload = GoogleJsonWebSignature.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
            var userExisting = _userService.GetByEmail(payload.Email);
            if (userExisting == null)
            {
                var user = _mapper.Map<UserView, UserDTO>(userView);
                user.Email = payload.Email;
                user.EmailConfirmed = true;
                user.Name = payload.Name;
                await _userService.Create(user);
            }
            var result = _authService.AuthenticateGoogleFacebookUser(payload.Email);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            var userInfo = _mapper.Map<UserInfo>(_userService.GetByEmail(payload.Email));
            userInfo.Token = result.Message;
            userInfo.Rating = _userService.GetRating(userInfo.Id);

            return Ok(userInfo);
        }


        [Authorize]
        [HttpPost("login_token")]
        public IActionResult Login()
        {
            var user = _authService.GetCurrentUser(HttpContext.User);
            var userInfo = _mapper.Map<UserInfo>(user);
            userInfo.Rating = _userService.GetRating(userInfo.Id);

            return Ok(userInfo);
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(LoginDto authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<LoginDto, UserDTO>(authRequest);
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

            var result = await _userService.ConfirmEmail(cache);

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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