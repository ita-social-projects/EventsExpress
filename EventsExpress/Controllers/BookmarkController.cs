using System;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = PolicyNames.UserPolicyName)]
[ApiController]
public class BookmarkController : ControllerBase
{
    private readonly IBookmarkService _service;

    public BookmarkController(IBookmarkService service) => _service = service;

    [HttpPost("[action]/{eventId:guid}")]
    public async Task<IActionResult> SaveEventBookmark([FromRoute] Guid eventId)
    {
        await _service.SaveEventToBookmarksAsync(eventId);
        return Ok();
    }

    [HttpPost("[action]/{eventId:guid}")]
    public async Task<IActionResult> DeleteEventBookmark([FromRoute] Guid eventId)
    {
        await _service.DeleteEventFromBookmarksAsync(eventId);
        return Ok();
    }
}
