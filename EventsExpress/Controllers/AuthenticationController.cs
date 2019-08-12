using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Helpers;
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
        private IUserService _userService;
        private IAuthServicre _authServicre;
        private IMapper  _mapper;

        public AuthenticationController(IUserService userSrv,
            IMapper mapper,
            IAuthServicre authServicre
            )
        {
            _userService = userSrv;
            _mapper = mapper;
            _authServicre = authServicre;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Post(
            LoginDto authRequest,
            [FromServices] IAuthServicre _authServise
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = _authServise.Authenticate(authRequest.Email, authRequest.Password);

            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            var user = _userService.GetByEmail(authRequest.Email);
            var responce = _mapper.Map<UserDTO, UserInfo>(user);
            //responce.Categories = user.Categories.Select(x => x.Category);

            responce.Token = result.Message;

            return Ok(responce);
        }


        [Authorize]
        [HttpPost("login_token")]
        public IActionResult Post(
            [FromServices] IAuthServicre _authServise
            )
        {
            var s = User.Identity.Name;     

            var user = _userService.GetByEmail(HttpContext.User.Claims?.First().Value);  
                                                                       
            var responce = _mapper.Map<UserDTO, UserInfo>(user);

            //responce.Token = result.Message;

            return Ok(responce);
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(LoginDto authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<LoginDto, UserDTO> (authRequest);
            user.PasswordHash = PasswordHasher.GenerateHash(authRequest.Password);
            var result = await _userService.Create(user);

            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
   
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> PasswordRecovery(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            var user = _userService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest("User with this email is not found");
            }
            var res = await _userService.PasswordRecover(user);
            if (!res.Successed)
            {
                return BadRequest(res.Message);
            }

            return Ok();
        }


        [AllowAnonymous]
        [HttpPost("[action]/{userid}/{token}")]
        public async Task<IActionResult> Verify(string userid, string token)
        {
            var cache = new CacheDTO { Token = token };

            var res = Guid.TryParse(userid, out cache.UserId);
            if (!res)
            {
                return BadRequest();
            }

            var result = await _userService.Verificate(cache);
            if (!result.Successed)
            {
                return BadRequest(ModelState);
            }
            if (result.Successed)
            {
                var user = _userService.GetById( cache.UserId);

                var responce = _mapper.Map<UserDTO, UserInfo>(user);
                responce.Token = _authServicre.FirstAuth(user).Message;
                responce.AfterEmailConfirmation = true;

                return Ok(responce);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword
            (ChangePasswordDto changePasswordDto, [FromServices] IAuthServicre _authServise
            )
        {
            var user = _authServise.GetCurrentUser(HttpContext.User);

            var check = _authServise.CheckPassword(changePasswordDto.OldPassword, user.PasswordHash);

            if (check == false)
            {
                return BadRequest(ModelState);
            }

            user.PasswordHash = PasswordHasher.GenerateHash(changePasswordDto.NewPassword);

            var result = await _userService.Update(user);

            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
        
    }
}