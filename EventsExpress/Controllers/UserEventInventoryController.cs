using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Policies;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyNames.UserPolicyName)]
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

        /// <summary>
        /// This method have to return all the items of event's inventory that taken by user.
        /// </summary>
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <returns>All the items of event's inventory.</returns>
        /// <response code="200">Return IEnumerable UserEventInventoryViewModel.</response>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMarkItemsByEventId(Guid eventId)
        {
            return Ok(await _userEventInventoryService.GetAllMarkItemsByEventId(eventId));
        }

        /// <summary>
        /// This method have to mark item as taken by user.
        /// </summary>
        /// <param name="model">Param model defines UserEventInventoryViewModel model.</param>
        /// <returns>The method returns marked items as taken.</returns>
        /// <response code="200">Marking item from inventar proces success.</response>
        /// <response code="400">If Marking item from inventar process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> MarkItemAsTakenByUser([FromBody] UserEventInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userEventInventoryService.MarkItemAsTakenByUser(_mapper.Map<UserEventInventoryViewModel, UserEventInventoryDto>(model));

            return Ok();
        }

        /// <summary>
        /// This method is for cancel mark item as taken by user.
        /// </summary>
        /// <param name="model">Param model defines UserEventInventoryViewModel model.</param>
        /// <returns>The method returns deleted inventory item.</returns>
        /// <response code="200">Cancel mark item as taken by user proces success.</response>
        /// <response code="400">If model param did not pass validation.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] UserEventInventoryViewModel model)
        {
            await _userEventInventoryService.Delete(_mapper.Map<UserEventInventoryViewModel, UserEventInventoryDto>(model));
            return Ok();
        }

        /// <summary>
        /// This method is for edit quantity of item that takan by user.
        /// </summary>
        /// <param name="model">Param model defines UserEventInventoryViewModel model.</param>
        /// <returns>The method returns edited inventory item.</returns>
        /// <response code="200">Edit proces success.</response>
        /// <response code="400">If model param did not pass validation.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromBody] UserEventInventoryViewModel model)
        {
            await _userEventInventoryService.Edit(_mapper.Map<UserEventInventoryViewModel, UserEventInventoryDto>(model));
            return Ok();
        }
    }
}
