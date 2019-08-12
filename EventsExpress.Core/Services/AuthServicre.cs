using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Helpers;
using EventsExpress.Db.IRepo;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class AuthServicre : IAuthServicre
    {
        private readonly IUserService _userServicre;
        private readonly IJwtSigningEncodingKey _signingEncodingKey;

        public AuthServicre(
            IUserService userSrv, 
            IJwtSigningEncodingKey signingEncodingKey)
        {
            _userServicre = userSrv;
            _signingEncodingKey = signingEncodingKey;
        }


        public OperationResult Authenticate(string email, string password)
        {
            var user = _userServicre.GetByEmail(email);
            if (user == null)
            {
                return new OperationResult(false, $"User with email: {email} not found", "email");
            }

            if (user.IsBlocked)
            {
                return new OperationResult(false, $"{email}, your account was blocked.", "email");
            }

            // validate password
            var passwordValid = this.VerifyPassword(password, user.PasswordHash);
            if (!passwordValid)
            {
                return new OperationResult(false, "Invalid password", "Password");
            }

            var token = this.GenerateJWT(user);

            return new OperationResult(true, token, "");
        }


        private bool VerifyPassword(string actualPassword, string hashedPassword)
        {
            return hashedPassword == PasswordHasher.GenerateHash(actualPassword);
        }


        private string GenerateJWT(UserDTO user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: new SigningCredentials(
                        _signingEncodingKey.GetKey(),
                        _signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
