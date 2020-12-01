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

        [HttpPost("[action]")]
        public IActionResult GetAllMarksByItemId(Guid itemId)
        {
            return Ok(_userEventInventoryService.GetAllMarksByItemId(itemId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MarkItemAsTakenByUser(UserEventInventoryDto model)
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
