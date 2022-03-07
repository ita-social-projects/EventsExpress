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
    private readonly AppDbContext dbContext;
    private readonly ISecurityContext securityContext;

    public BookmarkService(AppDbContext dbDbContext, ISecurityContext securityContext)
    {
        (this.dbContext, this.securityContext) = (dbDbContext, securityContext);
    }

    public async Task SaveEventToBookmarksAsync(Guid eventId)
    {
        var userId = securityContext.GetCurrentUserId();
        var bookmark = new EventBookmark { UserFromId = userId, EventId = eventId };

        if (dbContext.EventBookmarks.Any(b => b.UserFromId == bookmark.UserFromId && b.EventId == bookmark.EventId))
        {
            return;
        }

        dbContext.EventBookmarks.Add(bookmark);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteEventFromBookmarksAsync(Guid eventId)
    {
        var userId = securityContext.GetCurrentUserId();
        var bookmark = await dbContext.EventBookmarks.FirstOrDefaultAsync(
            b => b.UserFromId == userId && b.EventId == eventId);

        if (bookmark is null)
        {
            return;
        }

        dbContext.EventBookmarks.Remove(bookmark);
        await dbContext.SaveChangesAsync();
    }
}
