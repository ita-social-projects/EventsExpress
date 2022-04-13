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
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventsExpress.Core.Services
{
    public class TokenService : BaseService<UserToken>, ITokenService
    {
        private readonly IJwtSigningEncodingKey _signingEncodingKey;
        private readonly IOptions<JwtOptionsModel> _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIpProviderService _ipProviderService;
        private readonly IMapper _mapper;

        public TokenService(
            AppDbContext context,
            IMapper mapper,
            IOptions<JwtOptionsModel> opt,
            IJwtSigningEncodingKey jwtSigningEncodingKey,
            IHttpContextAccessor httpContextAccessor,
            IIpProviderService ipProviderService)
            : base(context, mapper)
        {
            _jwtOptions = opt;
            _signingEncodingKey = jwtSigningEncodingKey;
            _httpContextAccessor = httpContextAccessor;
            _ipProviderService = ipProviderService;
            _mapper = mapper;
        }

        private string IpAddress => _ipProviderService.GetIpAdress();

        public string GenerateAccessToken(Account account)
        {
            var lifeTime = _jwtOptions.Value.LifeTime;
            var claims = GetClaims(account);
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
            if (refreshToken.Expires < DateTime.Now && refreshToken.Revoked == null)
            {
                return null;
            }

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken();
            refreshToken.Revoked = DateTime.Now;
            refreshToken.RevokedByIp = IpAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.RefreshTokens.Add(newRefreshToken);

            await Context.SaveChangesAsync();

            // generate new jwt
            var jwtToken = GenerateAccessToken(account);
            return new AuthenticateResponseModel(jwtToken, newRefreshToken.Token);
        }

        private IEnumerable<Claim> GetClaims(Account account)
        {
            List<Claim> claims = new List<Claim>();
            if (account.UserId != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, $"{account.UserId}"));
            }

            claims.Add(new Claim(ClaimTypes.Sid, $"{account.Id}"));
            var roles = account.AccountRoles.Select(ar => ar.Role);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return claims.ToArray();
        }

        public UserToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return new UserToken
            {
                Type = TokenType.RefreshToken,
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
            var refreshToken = Context.UserTokens.SingleOrDefault(rt => rt.Token == token);

            // return false if token is not active
            if (refreshToken == null || !_mapper.Map<RefreshTokenDto>(refreshToken).IsActive)
            {
                return false;
            }

            // revoke token and save
            refreshToken.Revoked = DateTime.Now;
            refreshToken.RevokedByIp = IpAddress;
            await Context.SaveChangesAsync();
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");

            return true;
        }

        public async Task GenerateEmailConfirmationToken(string token, Guid accountId)
        {
            var emailToken = new UserToken
            {
                Type = TokenType.EmailConfirmationToken,
                Token = token,
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                AccountId = accountId,
            };

            Context.UserTokens.Add(emailToken);

            await Context.SaveChangesAsync();
        }
    }
}
