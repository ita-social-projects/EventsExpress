using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace EventsExpress.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly IJwtSigningEncodingKey _signingEncodingKey;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;


        public TokenService(IConfiguration configuration, IJwtSigningEncodingKey jwtSigningEncodingKey, IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _configuration = configuration;
            _signingEncodingKey = jwtSigningEncodingKey;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public string GenerateAccessToken(UserDTO user)
        {
            var lifeTime = _configuration.GetValue<int>("JWTOptions:LifeTime");
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(lifeTime),
                signingCredentials: new SigningCredentials(
                        _signingEncodingKey.GetKey(),
                        _signingEncodingKey.SigningAlgorithm)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.Now.AddDays(7),
                    Created = DateTime.Now
                };
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var signingKey = new SigningSymmetricKey(_configuration.GetValue<string>("JWTOptions:SecretKey"));

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingDecodingKey.GetKey(),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        public async Task<AuthenticateResponseModel> RefreshToken(string token)
        {
            var user = _userService.GetUserByRefreshToken(token);
            // return null if no user found with token
            if (user == null) return null;
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            
            // return null if token is no longer active
            if (!_mapper.Map<RefreshTokenDTO>(refreshToken).IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken();
            refreshToken.Revoked = DateTime.Now;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens = new List<RefreshToken> { newRefreshToken,refreshToken };

            await _userService.Update(user);

            // generate new jwt
            var jwtToken = GenerateAccessToken(user);
            return new AuthenticateResponseModel(jwtToken, newRefreshToken.Token);
        }
        public void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refreshToken");
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", token, cookieOptions);
           
        }
    }
}
