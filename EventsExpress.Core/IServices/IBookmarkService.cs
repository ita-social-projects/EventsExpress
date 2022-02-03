using System;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices;

public interface IBookmarkService
{
    Task SaveEventToBookmarksAsync(Guid eventId);

    Task DeleteEventFromBookmarksAsync(Guid eventId);
}
