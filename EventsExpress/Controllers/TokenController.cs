using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using EventsExpress.Core.Extensions;
using Microsoft.AspNetCore.Mvc;


namespace EventsExpress.Controllers
{
    /// <summary>
    /// Controller using for generate and refreshing tokens
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        /// <summary>
        /// ctor of TokenController
        /// </summary>
        /// <param name="userSrv"></param>
        /// <param name="authSrv"></param>
        /// <param name="tokenService"></param>
        public TokenController(
            IUserService userSrv,
            IAuthService authSrv,
            ITokenService tokenService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userService = userSrv;
            _authService = authSrv;
            _tokenService = tokenService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// action using for refresh token
        /// </summary>
        /// <returns>return new jwt and refresh token</returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var token = Request.Cookies["refreshToken"];

            var user = _userService.GetUserByRefreshToken(token);
            if (user == null) return BadRequest();

            var refreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == Request.Cookies["refreshToken"]);

            // return false if token is not active
            if (!_mapper.Map<RefreshTokenDTO>( refreshToken).IsActive || refreshToken == null) return BadRequest();

            // revoke token and save
            refreshToken.Revoked = DateTime.Now;
            user.RefreshTokens = new List<RefreshToken> { refreshToken };
            await _userService.Update(user);
           Response.Cookies.Delete("refreshToken");
            return Ok();
        }
    }
}
