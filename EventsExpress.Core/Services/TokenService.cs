using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventsExpress.Core.Services
{
    public class TokenService : BaseService<RefreshToken>, ITokenService
    {
        private readonly IJwtSigningEncodingKey _signingEncodingKey;
        private readonly IOptions<JwtOptionsModel> _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TokenService(
            AppDbContext context,
            IMapper mapper,
            IOptions<JwtOptionsModel> opt,
            IJwtSigningEncodingKey jwtSigningEncodingKey,
            IHttpContextAccessor httpContextAccessor)
            : base(context, mapper)
        {
            _jwtOptions = opt;
            _signingEncodingKey = jwtSigningEncodingKey;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private string IpAddress
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    return _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
                }
                else
                {
                    return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                }
            }
        }

        public string GenerateAccessToken(Account account, string email)
        {
            var lifeTime = _jwtOptions.Value.LifeTime;
            var claims = GetClaims(account, email);
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(lifeTime),
                signingCredentials: new SigningCredentials(
                        _signingEncodingKey.GetKey(),
                        _signingEncodingKey.SigningAlgorithm));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<AuthenticateResponseModel> RefreshToken(string token)
        {
            var account = Context.Accounts
                .Include(a => a.User)
                .Include(a => a.AccountRoles)
                    .ThenInclude(ar => ar.Role)
                .Include(a => a.RefreshTokens)
                .SingleOrDefault(a => a.RefreshTokens.Any(t => t.Token.Equals(token)));

            // return null if no user found with token
            if (account == null)
            {
                return null;
            }

            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!_mapper.Map<RefreshTokenDto>(refreshToken).IsActive)
            {
                return null;
            }

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken(refreshToken.Email);
            refreshToken.Revoked = DateTime.Now;
            refreshToken.RevokedByIp = IpAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            await Context.SaveChangesAsync();

            // generate new jwt
            var jwtToken = GenerateAccessToken(account, refreshToken.Email);
            return new AuthenticateResponseModel(jwtToken, newRefreshToken.Token);
        }

        private Claim[] GetClaims(Account account, string email)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, account.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, email));
            var roles = account.AccountRoles.Select(ar => ar.Role);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            return claims.ToArray();
        }

        public RefreshToken GenerateRefreshToken(string email)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = IpAddress,
            };
        }

        public ClaimsPrincipal GetPrincipalFromJwt(string token)
        {
            var signingKey = new SigningSymmetricKey(_jwtOptions.Value.SecretKey);

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingDecodingKey.GetKey(),
                ValidateLifetime = false,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<bool> RevokeToken(string token)
        {
            var refreshToken = Context.RefreshTokens.SingleOrDefault(rt => rt.Token == token);

            // return false if token is not active
            if (!_mapper.Map<RefreshTokenDto>(refreshToken).IsActive || refreshToken == null)
            {
                return false;
            }

            // revoke token and save
            refreshToken.Revoked = DateTime.Now;
            refreshToken.RevokedByIp = IpAddress;
            await Context.SaveChangesAsync();
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refreshToken");

            return true;
        }

        public void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7),
                Secure = true,
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refreshToken");
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
