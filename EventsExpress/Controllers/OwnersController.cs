using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
            var res = await _eventOwnersService.DeleteOwnerFromEvent(userId, eventId);
            if (res.Successed)
            {
                return Ok(res.Property);
            }


            return BadRequest();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> PromoteToOwner(Guid userId, Guid eventId)
        {
            var res = await _eventOwnersService.PromoteToOwner(userId, eventId);

            if (res.Successed)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
