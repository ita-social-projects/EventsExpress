using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserEventInventoryController : Controller
    {
        private readonly IUserEventInventoryService _userEventInventoryService;
        private readonly IMapper _mapper;

        public UserEventInventoryController(
            IUserEventInventoryService userEventInventoryService,
            IMapper mapper)
        {
            _userEventInventoryService = userEventInventoryService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public IActionResult GetAllMarkItemsByEventId(Guid eventId)
        {
            return Ok(_userEventInventoryService.GetAllMarkItemsByEventId(eventId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MarkItemAsTakenByUser([FromBody] UserEventInventoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userEventInventoryService.MarkItemAsTakenByUser(_mapper.Map<UserEventInventoryDto, UserEventInventoryDTO>(model));
            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }
    }
}
