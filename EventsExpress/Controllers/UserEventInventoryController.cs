using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> MarkItemAsTakenByUser([FromBody] UserEventInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userEventInventoryService.MarkItemAsTakenByUser(_mapper.Map<UserEventInventoryViewModel, UserEventInventoryDTO>(model));

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] UserEventInventoryViewModel model)
        {
            await _userEventInventoryService.Delete(_mapper.Map<UserEventInventoryViewModel, UserEventInventoryDTO>(model));
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromBody] UserEventInventoryViewModel model)
        {
            await _userEventInventoryService.Edit(_mapper.Map<UserEventInventoryViewModel, UserEventInventoryDTO>(model));
            return Ok();
        }
    }
}
