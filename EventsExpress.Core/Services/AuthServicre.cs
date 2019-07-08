using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Helpers;
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
        // private readonly IUnitOfWork _uow
        private readonly IJwtSigningEncodingKey _signingEncodingKey;

        public AuthServicre(IJwtSigningEncodingKey signingEncodingKey)
        {
            //_uow = uow;   
            _signingEncodingKey = signingEncodingKey;
        }


        public OperationResult Authenticate(string email, string password)
        {
            // next must be replased for REal user search:
            User user = new User { Email = "fake email", PasswordHash = PasswordHasher.GenerateHash("password")};
            
            if (user == null)
            {
                return new OperationResult(false, $"User with email: {email} not found", "email");
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

        private string GenerateJWT(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                        _signingEncodingKey.GetKey(),
                        _signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
