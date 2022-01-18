using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.IServices;
using EventsExpress.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyNames.UserPolicyName)]
    [ApiController]
    public class OrganizersController : Controller
    {
        private readonly IEventOwnersService _eventOwnersService;

        public OrganizersController(IEventOwnersService eventOwnersService)
        {
            _eventOwnersService = eventOwnersService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteFromOrganizers(Guid userId, Guid eventId)
        {
            await _eventOwnersService.DeleteOwnerFromEvent(userId, eventId);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> PromoteToOrganizer(Guid userId, Guid eventId)
        {
            await _eventOwnersService.PromoteToOwner(userId, eventId);

            return Ok();
        }
    }
}
