﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Enums;
using EventsExpress.Policies;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    /// <summary>
    /// AccountController is used to add several authorization methods to one account,
    /// change roles and block/unblock users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IGoogleSignatureVerificator _googleSignatureVerificator;
        private readonly ISecurityContext _securityContextService;

        public AccountController(
            IMapper mapper,
            IAccountService accountService,
            IGoogleSignatureVerificator googleSignatureVerificator,
            ISecurityContext securityContextService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _googleSignatureVerificator = googleSignatureVerificator;
            _securityContextService = securityContextService;
        }

        /// <summary>
        /// This method looks for account-bound authorization methods.
        /// </summary>
        /// <returns>The method returns all linked authorization methods.</returns>
        /// <response code="200">Return List of linked auth.</response>
        /// <response code="400">If authentication process failed.</response>
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLinkedAuth()
        {
            var accountId = GetCurrentAccountId();
            var res = await _accountService.GetLinkedAuth(accountId);

            return Ok(_mapper.Map<IEnumerable<AuthViewModel>>(res));
        }

        /// <summary>
        /// This method allow to add an Google-authorization method to an existing account.
        /// </summary>
        /// <param name="model">Param model defines AuthGoogleViewModel.</param>
        /// <returns>This method adds a google login to account.</returns>
        /// <response code="200">Return Ok.</response>
        /// <response code="400">If authentication process failed.</response>
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddGoogleLogin(AuthGoogleViewModel model)
        {
            await _googleSignatureVerificator.Verify(model.TokenId);

            var accountId = GetCurrentAccountId();

            await _accountService.AddAuth(accountId, model.Email, AuthExternalType.Google);

            return Ok();
        }

        /// <summary>
        /// This method allow to add an Local-authorization method to an existing account.
        /// </summary>
        /// <param name="model">Param model defines LoginViewModel.</param>
        /// <returns>This method adds a local login to account.</returns>
        /// <response code="200">Return Ok.</response>
        /// <response code="400">If authentication process failed.</response>
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddLocalLogin(LoginViewModel model)
        {
            var accountId = GetCurrentAccountId();

            await _accountService.AddAuth(accountId, model.Email, model.Password);

            return Ok();
        }

        /// <summary>
        /// This method allow to add an Facebook-authorization method to an existing account.
        /// </summary>
        /// <param name="model">Param model defines AuthExternalViewModel.</param>
        /// <returns>This method adds a facebook login to account.</returns>
        /// <response code="200">Return Ok.</response>
        /// <response code="400">If authentication process failed.</response>
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddFacebookLogin(AuthExternalViewModel model)
        {
            var accountId = GetCurrentAccountId();

            await _accountService.AddAuth(accountId, model.Email, AuthExternalType.Facebook);

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
        [Authorize(Policy = PolicyNames.AdminPolicyName)]
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
        [Authorize(Policy = PolicyNames.AdminPolicyName)]
        public async Task<IActionResult> Block(Guid userId)
        {
            await _accountService.Block(userId);

            return Ok();
        }

        /// <summary>
        /// This method allows admin to change user roles.
        /// </summary>
        /// <param name="model">Param model defines ChangeRoleWiewModel.</param>
        /// <returns>The method change user roles.</returns>
        /// <response code="200">Change roles is succesful.</response>
        /// <response code="400">Change roles is failed.</response>
        [HttpPost("[action]")]
        [Authorize(Policy = PolicyNames.AdminPolicyName)]
        public async Task<IActionResult> ChangeRoles(ChangeRoleWiewModel model)
        {
            var newRoles = _mapper.Map<IEnumerable<Db.Entities.Role>>(model.Roles);

            await _accountService.ChangeRole(model.UserId, newRoles);

            return Ok();
        }

        [NonAction]
        private Guid GetCurrentAccountId() =>
            _securityContextService.GetCurrentAccountId();
    }
}
