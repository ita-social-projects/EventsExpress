using System;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Filters;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "UserPolicy")]
    [ApiController]
    public class EventStatusHistoryController : ControllerBase
    {
        private readonly IEventStatusHistoryService _eventStatusHistoryService;

        public EventStatusHistoryController(
            IEventStatusHistoryService eventStatusHistoryService)
        {
            _eventStatusHistoryService = eventStatusHistoryService;
        }

        /// <summary>
        /// This method is for change event status.
        /// </summary>
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <param name="eventStatus">Param eventStatus defines the event status.</param>
        /// <returns>The method returns event with new status.</returns>
        /// <response code="200">Status changing succesful.</response>
        /// <response code="400">Status changing failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilterAttribute]
        public async Task<IActionResult> SetStatus(Guid eventId, EventStatusHistoryViewModel eventStatus)
        {
            await _eventStatusHistoryService.SetStatusEvent(eventStatus.EventId, eventStatus.Reason, eventStatus.EventStatus);

            return Ok(eventStatus);
        }
    }
}
