using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Helpers;

namespace EventsExpress.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserService userSrv,
            ITokenService tokenService)
        {
            _userService = userSrv;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponseModel> AuthenticateUserFromExternalProvider(string email)
        {
            UserDto user = _userService.GetByEmail(email);

            if (user == null)
            {
                throw new EventsExpressException($"User with email: {email} not found");
            }

            if (user.IsBlocked)
            {
                throw new EventsExpressException($"{email}, your account was blocked.");
            }

            var jwtToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // save refresh token
            user.RefreshTokens = new List<RefreshToken> { refreshToken };
            await _userService.Update(user);
            return new AuthenticateResponseModel(jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponseModel> Authenticate(string email, string password)
        {
            var user = _userService.GetByEmail(email);
            if (user == null)
            {
                throw new EventsExpressException("Incorrect login or password");
            }

            if (user.IsBlocked)
            {
                throw new EventsExpressException($"{email}, your account was blocked.");
            }

            if (!user.EmailConfirmed)
            {
                throw new EventsExpressException($"{email} is not confirmed, please confirm");
            }

            if (!VerifyPassword(user, password))
            {
                throw new EventsExpressException("Incorrect login or password");
            }

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // save refresh token
            user.RefreshTokens = new List<RefreshToken> { refreshToken };
            await _userService.Update(user);

            return new AuthenticateResponseModel(jwtToken, refreshToken.Token);
        }

        public async Task<AuthenticateResponseModel> FirstAuthenticate(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new EventsExpressException("User not found");
            }

            var jwtToken = _tokenService.GenerateAccessToken(userDto);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // save refresh token
            userDto.RefreshTokens = new List<RefreshToken> { refreshToken };

            await _userService.Update(userDto);

            return new AuthenticateResponseModel(jwtToken, refreshToken.Token);
        }

        public async Task ChangePasswordAsync(UserDTO userDto, string oldPassword, string newPassword)
        {
            if (VerifyPassword(userDto, oldPassword))
            {
                userDto.Salt = PasswordHasher.GenerateSalt();
                userDto.PasswordHash = PasswordHasher.GenerateHash(newPassword, userDto.Salt);
                await _userService.Update(userDto);
                return;
            }

            throw new EventsExpressException("Invalid password");
        }

        public UserDto GetCurrentUser(ClaimsPrincipal userClaims)
        {
            Claim emailClaim = userClaims.FindFirst(ClaimTypes.Email);

            if (string.IsNullOrEmpty(emailClaim?.Value))
            {
                return null;
            }

            return _userService.GetByEmail(emailClaim.Value);
        }

        private static bool VerifyPassword(UserDto user, string actualPassword) =>
            user.PasswordHash == PasswordHasher.GenerateHash(actualPassword, user.Salt);
    }
}
