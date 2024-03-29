﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Role = EventsExpress.Db.Entities.Role;

namespace EventsExpress.Core.Services
{
    public class AuthService : BaseService<Account>, IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISecurityContext _securityContext;

        public AuthService(
            AppDbContext context,
            IMapper mapper,
            IUserService userSrv,
            ITokenService tokenService,
            IEmailService emailService,
            IMediator mediator,
            IPasswordHasher passwordHasher,
            ISecurityContext securityContext)
            : base(context, mapper)
        {
            _userService = userSrv;
            _tokenService = tokenService;
            _emailService = emailService;
            _mediator = mediator;
            _passwordHasher = passwordHasher;
            _securityContext = securityContext;
        }

        public async Task<AuthenticateResponseModel> Authenticate(string email, AuthExternalType type)
        {
            var account = Context.Accounts
                .Include(a => a.AuthExternal)
                .Include(a => a.RefreshTokens)
                .Include(a => a.AccountRoles)
                    .ThenInclude<Account, AccountRole, Role>(ar => ar.Role)
                .FirstOrDefault(a => a.AuthExternal.Any(x => x.Email == email && x.Type == type));

            if (account == null)
            {
                throw new EventsExpressException($"Account not found");
            }

            if (account.IsBlocked)
            {
                throw new EventsExpressException($"Your account was blocked");
            }

            var jwtToken = _tokenService.GenerateAccessToken(account);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // save refresh token
            account.RefreshTokens.Add(refreshToken);
            await Context.SaveChangesAsync();
            return new AuthenticateResponseModel(jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponseModel> Authenticate(string email, string password)
        {
            var account = Context.Accounts
                .Include(a => a.AuthLocal)
                .Include(a => a.RefreshTokens)
                .Include(a => a.AccountRoles)
                    .ThenInclude(ar => ar.Role)
                .Where(a => a.AuthLocal.Email == email)
                .FirstOrDefault();
            if (account == null)
            {
                throw new EventsExpressException("Incorrect login or password");
            }

            if (account.IsBlocked)
            {
                throw new EventsExpressException("Your account was blocked.");
            }

            if (!account.AuthLocal.EmailConfirmed)
            {
                throw new EventsExpressException($"{email} is not confirmed, please confirm");
            }

            if (!VerifyPassword(account.AuthLocal, password))
            {
                throw new EventsExpressException("Incorrect login or password");
            }

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _tokenService.GenerateAccessToken(account);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // save refresh token
            account.RefreshTokens.Add(refreshToken);
            await Context.SaveChangesAsync();
            return new AuthenticateResponseModel(jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponseModel> EmailConfirmAndAuthenticate(Guid accountId, string token)
        {
            var localAccountId = Context.AuthLocal.FirstOrDefault(al => al.AccountId == accountId)?.AccountId;
            var userToken = Context.UserTokens
                            .FirstOrDefault(rt => rt.Token == token && rt.AccountId == localAccountId);

            var account = await ConfirmEmail(userToken);
            var jwtToken = _tokenService.GenerateAccessToken(account);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // save refresh token
            account.RefreshTokens.Add(refreshToken);
            await Context.SaveChangesAsync();
            return new AuthenticateResponseModel(jwtToken, refreshToken.Token);
        }

        public async Task ChangePasswordAsync(string oldPassword, string newPassword)
        {
            var authLocal = Context.AuthLocal
                .First(x => x.AccountId == _securityContext.GetCurrentAccountId());

            authLocal.Salt = _passwordHasher.GenerateSalt();
            authLocal.PasswordHash = _passwordHasher.GenerateHash(newPassword, authLocal.Salt);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> CanRegister(string email)
        {
            return !(await Context.AuthLocal.AnyAsync(al => al.Email == email));
        }

        public async Task<Guid> Register(RegisterDto registerDto)
        {
            var account = Mapper.Map<Account>(registerDto);
            account.AccountRoles = new[] { new AccountRole { RoleId = Db.Enums.Role.User } };
            var result = Insert(account);
            await Context.SaveChangesAsync();
            await _mediator.Publish(new RegisterVerificationMessage(account.AuthLocal));

            return result.Id;
        }

        public async Task RegisterComplete(RegisterCompleteDto registerCompleteDto)
        {
            registerCompleteDto.AccountId = _securityContext.GetCurrentAccountId();
            var userDto = Mapper.Map<UserDto>(registerCompleteDto);
            await _userService.Create(userDto);
        }

        public async Task PasswordRecover(string email)
        {
            var authLocal = Context.AuthLocal.FirstOrDefault(al => al.Email == email);
            if (authLocal == null)
            {
                throw new EventsExpressException("Not found");
            }

            var newPassword = Guid.NewGuid().ToString();
            authLocal.Salt = _passwordHasher.GenerateSalt();
            authLocal.PasswordHash = _passwordHasher.GenerateHash(newPassword, authLocal.Salt);

            await Context.SaveChangesAsync();
            await _emailService.SendEmailAsync(new EmailDto
            {
                Subject = "EventsExpress password recovery",
                RecepientEmail = authLocal.Email,
                MessageText = $"Hello, {authLocal.Email}.\nYour new Password is: {newPassword}",
            });
        }

        private bool VerifyPassword(AuthLocal authLocal, string actualPassword) =>
           authLocal.PasswordHash == _passwordHasher.GenerateHash(actualPassword, authLocal.Salt);

        private async Task<Account> ConfirmEmail(UserToken userToken)
        {
            if (string.IsNullOrEmpty(userToken?.Token))
            {
                throw new EventsExpressException("Token is null or empty");
            }

            var authLocal = Context.AuthLocal
                .Include(al => al.Account)
                    .ThenInclude(a => a.AccountRoles)
                        .ThenInclude(ar => ar.Role)
                .Include(al => al.Account)
                    .ThenInclude(a => a.RefreshTokens)
                .FirstOrDefault(al => al.AccountId == userToken.AccountId);
            if (authLocal == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            authLocal.EmailConfirmed = true;
            await Context.SaveChangesAsync();

            return authLocal.Account;
        }
    }
}
