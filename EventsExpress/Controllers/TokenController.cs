using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    /// <summary>
    /// Controller using for generate and refreshing tokens.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// action using for refresh token.
        /// </summary>
        /// <returns>return new jwt and refresh token.</returns>
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _tokenService.RefreshToken(refreshToken);
            if (response == null)
            {
                return Unauthorized();
            }

            _tokenService.SetTokenCookie(response.RefreshToken);
            return Ok(new { jwtToken = response.JwtToken });
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> Revoke()
        {
            var token = Request.Cookies["refreshToken"];
            var response = await _tokenService.RevokeToken(token);
            if (!response)
            {
                return NotFound(new { message = "Token not found" });
            }

            return Ok(new { message = "Token revoked" });
        }
    }
}
