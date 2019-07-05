using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult<string> Post(
            LoginDto authRequest,
            [FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            // 1. Check login model
            if (authRequest.Name != "Admin")
            {
                return BadRequest();
            }

            // 2. create claims for user
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, authRequest.Name),
                new Claim(ClaimTypes.Role, "admin")
            };

            // 3. Generate JWT.
            var token = new JwtSecurityToken(
                issuer: "EventsExpress",
                audience: "EventsExpress.Web",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        
    }
}