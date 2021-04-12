using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
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
        private readonly IGoogleSignatureVerificator _googleSignatureVerificator;

        public AccountController(
            IMapper mapper,
            IAuthService authSrv,
            IAccountService accountService,
            IGoogleSignatureVerificator googleSignatureVerificator)
        {
            _mapper = mapper;
            _authService = authSrv;
            _accountService = accountService;
            _googleSignatureVerificator = googleSignatureVerificator;
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
            await _googleSignatureVerificator.Verify(model.TokenId);

            var user = await _authService.GetCurrentUserAsync(HttpContext.User);

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

        /// <summary>
        /// This method is to unblock user.
        /// </summary>
        /// <param name="userId">Param userId defines the user identifier.</param>
        /// <returns>The method returns unblocked user.</returns>
        /// <response code="200">Block is succesful.</response>
        /// <response code="400">Block process failed.</response>
        [HttpPost("{userId}/[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Unblock(Guid userId)
        {
            await _accountService.Unblock(userId);

            return Ok();
        }

        /// <summary>
        /// This method is to block user.
        /// </summary>
        /// <param name="userId">Param userId defines the user identifier.</param>
        /// <returns>The method returns blocked user.</returns>
        /// <response code="200">Unblock is succesful.</response>
        /// <response code="400">Unblock process failed.</response>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Block(Guid userId)
        {
            await _accountService.Block(userId);

            return Ok();
        }

        /// <summary>
        /// This method allows admin to change user roles.
        /// </summary>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRoles(ChangeRoleWiewModel model)
        {
            var newRoles = _mapper.Map<IEnumerable<Db.Entities.Role>>(model.Roles);

            await _accountService.ChangeRole(model.UserId, newRoles);

            return Ok();
        }
    }
}
