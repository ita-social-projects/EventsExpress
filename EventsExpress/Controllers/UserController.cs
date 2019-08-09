using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.DTO;
using EventsExpress.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
     
        public UsersController(IUserService userSrv, IMapper mapper)
        {
           
            _userService = userSrv;
            _mapper = mapper;
        }


        [HttpGet("[action]")]
        [Authorize]
        public IActionResult SearchUsers([FromQuery]UsersFilterViewModel model)
        {


            if (model.PageSize == 0)
            {
                model.PageSize = 4;
            }

            int Count;

            var res = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserManageDto>>(_userService.GetAll(model, out Count));



            PageViewModel pageViewModel = new PageViewModel(Count, model.Page, model.PageSize);
            if (pageViewModel.PageNumber > pageViewModel.TotalPages)
            {
                return BadRequest();
            }
            IndexViewModel<UserManageDto> viewModel = new IndexViewModel<UserManageDto>
            {
                PageViewModel = pageViewModel,
                items = res
            };
    
            return Ok(viewModel);

        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromQuery]UsersFilterViewModel model)
        {


            if (model.PageSize == 0) {
                model.PageSize = 4;
            }

            int Count;

            var res = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserManageDto>>(_userService.GetAll(model, out Count));



            PageViewModel pageViewModel = new PageViewModel(Count, model.Page, model.PageSize);
            if (pageViewModel.PageNumber > pageViewModel.TotalPages)
            {
                return BadRequest();
            }
            IndexViewModel<UserManageDto> viewModel = new IndexViewModel<UserManageDto>
            {
                PageViewModel = pageViewModel,
                items = res
            };
            return Ok(viewModel);
    
        }

        [HttpGet("blocked")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetBlockedUsers()
        {
            var users = _userService.Get(u => u.IsBlocked);

            return Ok(users);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(Guid userId, Guid roleId)
        {
            var result = await _userService.ChangeRole(userId, roleId);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Unblock(Guid userId)
        {
            var result = await _userService.Unblock(userId);

            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Block(Guid userId)
        {
            //Guid id;
            //Guid.TryParse(userId, out id);
            var result = await _userService.Block(userId);

            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        #region My profile managment

        [HttpPost("[action]")]
        public async Task<IActionResult> EditUsername(UserInfo userInfo)
        {
            var user = _userService.GetByEmail(HttpContext.User.Claims?.First().Value);
            if (user == null)
            {
                return BadRequest();
            }

            string newName = userInfo.Name;
            if (string.IsNullOrEmpty(newName))
            {
                return BadRequest();
            } 

            user.Name = newName;

            var result = await _userService.Update(user);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditBirthday(UserInfo userInfo)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            DateTime newBirthday = userInfo.Birthday;
            user.Birthday = newBirthday;

            var result = await _userService.Update(user);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditGender(UserInfo userInfo)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            byte newGender = userInfo.Gender;
            user.Gender = (Gender)newGender;

            var result = await _userService.Update(user);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditUserCategory(UserInfo userInfo)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            IEnumerable<Category> newCategories = _mapper.Map<IEnumerable<CategoryDto>, IEnumerable<Category>>(userInfo.Categories);

            var result = await _userService.EditFavoriteCategories(user, newCategories);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest();      
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeAvatar([FromForm]IFormFile newAva)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            newAva = HttpContext.Request.Form.Files[0];
            
            var result = await _userService.ChangeAvatar(user.Id, newAva);
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        #endregion

        private UserDTO GetCurrentUser(ClaimsPrincipal userClaims)
        {
            string email = userClaims.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }
            return _userService.GetByEmail(email);
        }
    }
}