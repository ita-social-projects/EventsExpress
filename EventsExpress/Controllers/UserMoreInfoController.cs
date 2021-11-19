using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserMoreInfoController : ControllerBase
    {
        private readonly IUserMoreInfoService _userMoreInfoService;
        private readonly IMapper _mapper;

        public UserMoreInfoController(IUserMoreInfoService userService, IMapper mapper)
        {
            _userMoreInfoService = userService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] UserMoreInfoCreateViewModel userMoreInfo)
        {
            var newuserMoreInfo = _mapper.Map<UserMoreInfoDto>(userMoreInfo);

            await _userMoreInfoService.CreateAsync(newuserMoreInfo);

            return Ok();
        }
    }
}
