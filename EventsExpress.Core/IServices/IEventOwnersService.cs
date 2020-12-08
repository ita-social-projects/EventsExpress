using System;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IEventOwnersService
    {
        Task DeleteOwnerFromEvent(Guid userId, Guid eventId);

        Task PromoteToOwner(Guid userId, Guid eventId);
    }
}
