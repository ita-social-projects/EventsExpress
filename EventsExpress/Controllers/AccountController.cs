using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    /// <summary>
    /// AccountController is used to add several authorization methods to one account.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(
            IMapper mapper,
            IAuthService authSrv,
            IAccountService accountService)
        {
            _mapper = mapper;
            _authService = authSrv;
            _accountService = accountService;
        }

        /// <summary>
        /// This method looks for account-bound authorization methods.
        /// </summary>
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLinkedAuth()
        {
            var user = await _authService.GetCurrentUserAsync(HttpContext.User);
            var res = await _accountService.GetLinkedAuth(user.AccountId);

            return Ok(_mapper.Map<IEnumerable<AuthViewModel>>(res));
        }

        /// <summary>
        /// This method allow to add an Google-authorization method to an existing account.
        /// </summary>
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddGoogleLogin(AuthGoogleViewModel model)
        {
            var user = await _authService.GetCurrentUserAsync(HttpContext.User);

            await GoogleJsonWebSignature.ValidateAsync(
                model.TokenId, new GoogleJsonWebSignature.ValidationSettings());

            await _accountService.AddAuth(user.AccountId, model.Email, AuthExternalType.Google);

            return Ok();
        }

        /// <summary>
        /// This method allow to add an Local-authorization method to an existing account.
        /// </summary>
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddLocalLogin(LoginViewModel loginViewModel)
        {
            var user = await _authService.GetCurrentUserAsync(HttpContext.User);

            await _accountService.AddAuth(user.AccountId, loginViewModel.Email, loginViewModel.Password);

            return Ok();
        }

        /// <summary>
        /// This method allow to add an Facebook-authorization method to an existing account.
        /// </summary>
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddFacebookLogin(AuthExternalViewModel model)
        {
            var user = await _authService.GetCurrentUserAsync(HttpContext.User);

            await _accountService.AddAuth(user.AccountId, model.Email, AuthExternalType.Facebook);

            return Ok();
        }

        /// <summary>
        /// This method allow to add an Twitter-authorization method to an existing account.
        /// </summary>
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddTwitterLogin(AuthExternalViewModel model)
        {
            var user = await _authService.GetCurrentUserAsync(HttpContext.User);

            await _accountService.AddAuth(user.AccountId, model.Email, AuthExternalType.Twitter);

            return Ok();
        }
    }
}
