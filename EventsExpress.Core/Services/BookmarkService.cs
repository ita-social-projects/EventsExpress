using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services;

public class BookmarkService : IBookmarkService
{
    private readonly AppDbContext _dbContext;
    private readonly ISecurityContext _securityContext;

    public BookmarkService(AppDbContext dbDbContext, ISecurityContext securityContext) =>
        (_dbContext, _securityContext) = (dbDbContext, securityContext);

    public async Task SaveEventToBookmarksAsync(Guid eventId)
    {
        var userId = _securityContext.GetCurrentUserId();
        var bookmark = new EventBookmark { UserFromId = userId, EventId = eventId };

        if (_dbContext.EventBookmarks.Any(b => b.UserFromId == bookmark.UserFromId && b.EventId == bookmark.EventId))
        {
            return;
        }

        _dbContext.EventBookmarks.Add(bookmark);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteEventFromBookmarksAsync(Guid eventId)
    {
        var userId = _securityContext.GetCurrentUserId();
        var bookmark = await _dbContext.EventBookmarks.FirstOrDefaultAsync(
            b => b.UserFromId == userId && b.EventId == eventId);

        if (bookmark is null)
        {
            return;
        }

        _dbContext.EventBookmarks.Remove(bookmark);
        await _dbContext.SaveChangesAsync();
    }
}
