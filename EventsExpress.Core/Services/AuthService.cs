using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Helpers;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EventsExpress.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;


        public AuthService(
            IUserService userSrv,
            ITokenService tokenService

            )
        {
            _userService = userSrv;
            _tokenService = tokenService;
        }

        public async Task<(OperationResult opResult, AuthenticateResponseModel authResponseModel)> AuthenticateGoogleFacebookUser(string email)
        {
            var user = _userService.GetByEmail(email);
            if (user == null)
            {
                return (new OperationResult(false, $"User with email: {email} not found", "email"), null);
            }

            if (user.IsBlocked)
            {
                return (new OperationResult(false, $"{email}, your account was blocked.", "email"),null);
            }

            var jwtToken = _tokenService.GenerateAccessToken(user);
            string ipAddress = _tokenService.IpAddress();
            var refreshToken = _tokenService.GenerateRefreshToken(ipAddress);
             // save refresh token
            user.RefreshTokens = new List<RefreshToken> { refreshToken };
            await _userService.Update(user);
            return (new OperationResult(true, "", ""), new AuthenticateResponseModel(jwtToken, refreshToken.Token));
        }

        public async Task<(OperationResult opResult, AuthenticateResponseModel authResponseModel)> Authenticate(string email, string password)
        {
            var user = _userService.GetByEmail(email);
            if (user == null)
            {

                return (new OperationResult(false, "User not found", "email"), null);
            }

            if (user.IsBlocked)
            {
                return (new OperationResult(false, $"{email}, your account was blocked.", "email"), null);
            }

            if (!user.EmailConfirmed)
            {
                return (new OperationResult(false, $"{email} is not confirmed, please confirm", ""), null);
            }

            if (!VerifyPassword(user, password))
            {
                return (new OperationResult(false, "Invalid password", "Password"), null);
            }
            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _tokenService.GenerateAccessToken(user);
            string ipAddress = _tokenService.IpAddress();
            var refreshToken = _tokenService.GenerateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens = new List<RefreshToken> { refreshToken };
            await _userService.Update(user);

            return (new OperationResult(true, "", ""), new AuthenticateResponseModel(jwtToken, refreshToken.Token));
        }

        public async Task<(OperationResult opResult, AuthenticateResponseModel authResponseModel)> FirstAuthenticate(UserDTO userDto)
        {
            if (userDto == null)
            {
                return (new OperationResult(false, $"User with email: {userDto.Email} not found", "email"), null);
            }
            var jwtToken = _tokenService.GenerateAccessToken(userDto);
            string ipAddress = _tokenService.IpAddress();
            var refreshToken = _tokenService.GenerateRefreshToken(ipAddress);

            // save refresh token
            userDto.RefreshTokens = new List<RefreshToken> { refreshToken };

            await _userService.Update(userDto);

            return (new OperationResult(true, "", ""), new AuthenticateResponseModel(jwtToken, refreshToken.Token));
        }


        public async Task<OperationResult> ChangePasswordAsync(UserDTO userDto, string oldPassword, string newPassword)
        {
            if (VerifyPassword(userDto, oldPassword))
            {
                userDto.PasswordHash = PasswordHasher.GenerateHash(newPassword);

                return await _userService.Update(userDto);
            }
            return new OperationResult(false, "Invalid password", "");
        }

        public UserDTO GetCurrentUser(ClaimsPrincipal userClaims)
        {
            Claim emailClaim = userClaims.FindFirst(ClaimTypes.Email);
            if (emailClaim is null) return null;
            return 
                string.IsNullOrEmpty(emailClaim.Value)
                ? null 
                : _userService.GetByEmail(emailClaim.Value);
        }

        private static bool VerifyPassword(UserDTO user, string actualPassword) =>
            (user.PasswordHash == PasswordHasher.GenerateHash(actualPassword));

    }


}