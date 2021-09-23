using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ExtensionMethods;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;
        private readonly IGoogleSignatureVerificator _googleSignatureVerificator;

        public AuthenticationController(
            IMapper mapper,
            IAuthService authSrv,
            ITokenService tokenService,
            IAccountService accountService,
            IGoogleSignatureVerificator googleSignatureVerificator)
        {
            _mapper = mapper;
            _authService = authSrv;
            _tokenService = tokenService;
            _accountService = accountService;
            _googleSignatureVerificator = googleSignatureVerificator;
        }

        /// <summary>
        /// This method allows to log in to the API and generate an authentication token.
        /// </summary>
        /// <param name="authRequest">Param authRequest defines LoginViewModel.</param>
        /// <returns>The method performs Login operation.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public async Task<IActionResult> Login(LoginViewModel authRequest)
        {
            var authResponseModel = await _authService.Authenticate(authRequest.Email, authRequest.Password);
            HttpContext.SetTokenCookie(authResponseModel);

            return Ok(new { Token = authResponseModel.JwtToken });
        }

        /// <summary>
        /// This method is to login with facebook account.
        /// </summary>
        /// <param name="userView">Param userView defines UserViewModel.</param>
        /// <returns>The method performs Facebook Login operation.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> FacebookLogin(AccountViewModel userView)
        {
            await _accountService.EnsureExternalAccountAsync(userView.Email, AuthExternalType.Facebook);
            var authResponseModel = await _authService.Authenticate(userView.Email, AuthExternalType.Facebook);

            HttpContext.SetTokenCookie(authResponseModel);

            return Ok(new { Token = authResponseModel.JwtToken });
        }

        /// <summary>
        /// This method is to login with google account.
        /// </summary>
        /// <param name="model">Param userView defines AccountViewModel.</param>
        /// <returns>The method performs Google Login operation.</returns>
        /// /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> GoogleLogin(AccountViewModel model)
        {
            var payload = await _googleSignatureVerificator.Verify(model.TokenId);

            await _accountService.EnsureExternalAccountAsync(payload.Email, AuthExternalType.Google);
            var authResponseModel = await _authService.Authenticate(payload.Email, AuthExternalType.Google);

            HttpContext.SetTokenCookie(authResponseModel);

            return Ok(new { Token = authResponseModel.JwtToken });
        }

        /// <summary>
        /// This method is to login with twitter account.
        /// </summary>
        /// <param name="model">Param userView defines AccountViewModel.</param>
        /// <returns>The method performs Twitter Login operation.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> TwitterLogin([FromBody] AccountViewModel model)
        {
            await _accountService.EnsureExternalAccountAsync(model.Email, AuthExternalType.Twitter);
            var authResponseModel = await _authService.Authenticate(model.Email, AuthExternalType.Twitter);

            HttpContext.SetTokenCookie(authResponseModel);

            return Ok(new { Token = authResponseModel.JwtToken });
        }

        /// <summary>
        /// This method allows register user.
        /// </summary>
        /// <param name="authRequest">Param authRequest defines LoginViewModel.</param>
        /// <returns>The method performs Register operation.</returns>
        /// <response code="200">Register valid.</response>
        /// <response code="400">If register process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterBegin(LoginViewModel authRequest)
        {
            if (!await _authService.CanRegister(authRequest.Email))
            {
                throw new EventsExpressException("This email is already in use");
            }

            var accountNew = _mapper.Map<RegisterDto>(authRequest);
            var accountId = await _authService.Register(accountNew);

            return Ok(new { Id = accountId });
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterBindExternalAccount(RegisterBindViewModel authRequest)
        {
            var accountData = _mapper.Map<RegisterBindDto>(authRequest);
            var authResponseModel = await _authService.BindExternalAccount(accountData);

            HttpContext.SetTokenCookie(authResponseModel);

            return Ok(new { Token = authResponseModel.JwtToken });
        }

        /// <summary>
        /// This method allows complete registration.
        /// </summary>
        /// <param name="authRequest">Param authRequest defines LoginViewModel.</param>
        /// <returns>The method performs RegisterComplete operation.</returns>
        /// <response code="200">Register complete.</response>
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterComplete(RegisterCompleteViewModel authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profileData = _mapper.Map<RegisterCompleteDto>(authRequest);

            await _authService.RegisterComplete(profileData);

            // need to refresh user JWT because of the new userID claim has been added
            var refreshToken = Request.Cookies["refreshToken"];
            var authResponseModel = await _tokenService.RefreshToken(refreshToken);

            return Ok(new { Token = authResponseModel.JwtToken });
        }

        /// <summary>
        /// This method is for password recovery.
        /// </summary>
        /// <param name="email">Param email defines user email.</param>
        /// <returns>The method performs password recovery operation.</returns>
        /// <response code="200">Password recovery successful.</response>
        /// <response code="400">If password recover process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> PasswordRecovery(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new EventsExpressException("Incorrect email");
            }

            await _authService.PasswordRecover(email);

            return Ok();
        }

        /// <summary>
        /// This method is for email confirmation.
        /// </summary>
        /// <param name="authLocalId">Param userid defines user identifier.</param>
        /// <param name="token">Param token defines access token.</param>
        /// <returns>The method performs mail confirmation operation.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If email confirm process failed.</response>
        [AllowAnonymous]
        [HttpPost("verify/{authLocalId}/{token}")]
        public async Task<IActionResult> EmailConfirm(string authLocalId, string token)
        {
            if (!Guid.TryParse(authLocalId, out Guid id))
            {
                throw new EventsExpressException("User not found");
            }

            var authResponseModel = await _authService.EmailConfirmAndAuthenticate(id, token);

            HttpContext.SetTokenCookie(authResponseModel);

            return Ok(new { Token = authResponseModel.JwtToken });
        }

        /// <summary>
        /// This method is for change password.
        /// </summary>
        /// <param name="model">Param changePasswordDto ChangeViewModel.</param>
        /// <returns>The method performs password change operation.</returns>
        /// <response code="200">Password change successful.</response>
        /// <response code="400">If password change process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangeViewModel model)
        {
            await _authService.ChangePasswordAsync(model.OldPassword, model.NewPassword);

            return Ok();
        }
    }
}
