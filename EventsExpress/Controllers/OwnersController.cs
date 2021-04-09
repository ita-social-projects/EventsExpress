using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "UserPolicy")]
    [ApiController]
    public class OwnersController : Controller
    {
        private readonly IEventOwnersService _eventOwnersService;

        public OwnersController(IEventOwnersService eventOwnersService)
        {
            _eventOwnersService = eventOwnersService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteFromOwners(Guid userId, Guid eventId)
        {
            await _eventOwnersService.DeleteOwnerFromEvent(userId, eventId);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> PromoteToOwner(Guid userId, Guid eventId)
        {
            await _eventOwnersService.PromoteToOwner(userId, eventId);

            return Ok();
        }
    }
}
