using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Helpers;
using EventsExpress.ViewModels;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    /// <summary>
    /// AuthenticationController using for Authenticate users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IPhotoService _photoService;

        public AuthenticationController(
            IUserService userSrv,
            IMapper mapper,
            IAuthService authSrv,
            ITokenService tokenService,
            IPhotoService photoService)
        {
            _userService = userSrv;
            _mapper = mapper;
            _authService = authSrv;
            _tokenService = tokenService;
            _photoService = photoService;
        }

        /// <summary>
        /// This method allows to log in to the API and generate an authentication token.
        /// </summary>
        /// <param name="authRequest">Required.</param>
        /// <returns>UserInfo model.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public async Task<IActionResult> Login(LoginViewModel authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (opResult, authResponseModel) = await _authService.Authenticate(authRequest.Email, authRequest.Password);
            if (!opResult.Successed)
            {
                return BadRequest(opResult.Message);
            }

            var user = _userService.GetByEmail(authRequest.Email);
            var userInfo = _mapper.Map<UserInfoViewModel>(user);
            userInfo.Token = authResponseModel.JwtToken;
            _tokenService.SetTokenCookie(authResponseModel.RefreshToken);
            return Ok(userInfo);
        }

        /// <summary>
        /// This method is to login with facebook account.
        /// </summary>
        /// <param name="userView">Requireed.</param>
        /// <returns>UserInfo model.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> FacebookLogin(UserViewModel userView)
        {
            UserDTO userExisting = _userService.GetByEmail(userView.Email);

            if (userExisting == null && !string.IsNullOrEmpty(userView.Email))
            {
                var user = _mapper.Map<UserDTO>(userView);
                user.EmailConfirmed = true;
                user.Photo = await _photoService.AddPhotoByURL(userView.PhotoUrl);
                user.PhotoId = user.Photo.Id;
                await _userService.Create(user);
            }

            await SetPhoto(userExisting, userView.PhotoUrl);
            var (opResult, authResponseModel) = await _authService.AuthenticateUserFromExternalProvider(userView.Email);
            if (!opResult.Successed)
            {
                return BadRequest(opResult.Message);
            }

            var userInfo = _mapper.Map<UserInfoViewModel>(_userService.GetByEmail(userView.Email));
            userInfo.Token = authResponseModel.JwtToken;
            _tokenService.SetTokenCookie(authResponseModel.RefreshToken);
            return Ok(userInfo);
        }

        /// <summary>
        /// This method is to login with google account.
        /// </summary>
        /// <param name="userView">Requireed.</param>
        /// <returns>UserInfo model.</returns>
        /// /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> GoogleLogin([FromBody] UserViewModel userView)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                userView.TokenId, new GoogleJsonWebSignature.ValidationSettings());
            UserDTO userExisting = _userService.GetByEmail(payload.Email);

            if (userExisting == null && !string.IsNullOrEmpty(payload.Email))
            {
                var user = _mapper.Map<UserViewModel, UserDTO>(userView);
                user.Email = payload.Email;
                user.EmailConfirmed = true;
                user.Name = payload.Name;
                user.Photo = await _photoService.AddPhotoByURL(userView.PhotoUrl);
                user.PhotoId = user.Photo.Id;
                await _userService.Create(user);
            }

            await SetPhoto(userExisting, userView.PhotoUrl);
            var (opResult, authResponseModel) = await _authService.AuthenticateUserFromExternalProvider(payload.Email);
            if (!opResult.Successed)
            {
                return BadRequest(opResult.Message);
            }

            var userInfo = _mapper.Map<UserInfoViewModel>(_userService.GetByEmail(payload.Email));
            userInfo.Token = authResponseModel.JwtToken;
            _tokenService.SetTokenCookie(authResponseModel.RefreshToken);
            return Ok(userInfo);
        }

        /// <summary>
        /// This method is to login with twitter account.
        /// </summary>
        /// <param name="userView">Required.</param>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> TwitterLogin([FromBody] UserViewModel userView)
        {
            UserDTO userExisting = _userService.GetByEmail(userView.Email);

            if (!(userExisting is null) && !string.IsNullOrEmpty(userView.Email))
            {
                UserDTO user = _mapper.Map<UserDTO>(userView);
                user.EmailConfirmed = true;
                user.Photo = await _photoService.AddPhotoByURL(userView.PhotoUrl);
                user.PhotoId = user.Photo.Id;
                await _userService.Create(user);
            }

            await SetPhoto(userExisting, userView.PhotoUrl);
            var (opResult, authResponseModel) = await _authService.AuthenticateUserFromExternalProvider(userView.Email);
            if (!opResult.Successed)
            {
                return BadRequest(opResult.Message);
            }

            UserInfoViewModel userInfo = _mapper.Map<UserInfoViewModel>(_userService.GetByEmail(userView.Email));
            userInfo.Token = authResponseModel.JwtToken;
            _tokenService.SetTokenCookie(authResponseModel.RefreshToken);
            return Ok(userInfo);
        }

        private async Task<bool> SetPhoto(UserDTO userExisting, string urlPhoto)
        {
            if (userExisting != null)
            {
                if (userExisting.Photo == null)
                {
                    userExisting.Photo = await _photoService.AddPhotoByURL(urlPhoto);
                    userExisting.PhotoId = userExisting.Photo.Id;
                    await _userService.Update(userExisting);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method to refresh user status using only jwt access token.
        /// </summary>
        /// <returns>UserInfo model.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="401">If token is invalid.</response>
        [Authorize]
        [HttpPost("login_token")]
        public IActionResult Login()
        {
            var user = _authService.GetCurrentUser(HttpContext.User);
            return
            user == null
               ? (IActionResult)Unauthorized()
               : Ok(_mapper.Map<UserInfoViewModel>(user));
        }

        /// <summary>
        /// This method allows register user.
        /// </summary>
        /// <param name="authRequest">Required.</param>
        /// <response code="200">Register valid.</response>
        /// <response code="400">If register process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(LoginViewModel authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<LoginViewModel, UserDTO>(authRequest);
            user.PasswordHash = PasswordHasher.GenerateHash(authRequest.Password);
            var result = await _userService.Create(user);
            return
                !result.Successed
                ? (IActionResult)BadRequest(result.Message)
                : Ok();
        }

        /// <summary>
        /// This method is for password recovery.
        /// </summary>
        /// <param name="email">Required.</param>
        /// <response code="200">Password recovery succesful.</response>
        /// <response code="400">If password recover process failed.</response>
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

        /// <summary>
        /// This method is for email confirmation.
        /// </summary>
        /// <param name="userid">Required.</param>
        /// <param name="token">access token.</param>
        /// <returns>UserInfo model.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If emeil confirm process failed.</response>
        [AllowAnonymous]
        [HttpPost("verify/{userid}/{token}")]
        public async Task<IActionResult> EmailConfirm(string userid, string token)
        {
            var cache = new CacheDTO { Token = token };
            if (!Guid.TryParse(userid, out Guid userId))
            {
                return BadRequest();
            }

            cache.UserId = userId;

            var result = await _userService.ConfirmEmail(cache);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            var user = _userService.GetById(cache.UserId);
            var userInfo = _mapper.Map<UserDTO, UserInfoViewModel>(user);
            var (_, authResponseModel) = await _authService.FirstAuthenticate(user);
            userInfo.Token = authResponseModel.JwtToken;
            await _userService.Update(user);
            userInfo.AfterEmailConfirmation = true;
            return Ok(userInfo);
        }

        /// <summary>
        /// This method is for change password.
        /// </summary>
        /// <param name="changePasswordDto">Required.</param>
        /// <response code="200">Password change succesful.</response>
        /// <response code="400">If assword change process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangeViewModel changePasswordDto)
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
