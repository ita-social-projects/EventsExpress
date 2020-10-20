using System;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
        /// This method is for event cancelation.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        /// <response code="200">Cancelation succesful.</response>
        /// <response code="400">Cancelation failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Cancel(Guid eventId, string reason)
        {
            var result = await _eventStatusHistoryService.CancelEvent(eventId, reason);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
    }
}
