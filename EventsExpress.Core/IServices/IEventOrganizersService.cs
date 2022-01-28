using System;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IEventOrganizersService
    {
        Task DeleteOrganizerFromEvent(Guid userId, Guid eventId);

        Task PromoteToOrganizer(Guid userId, Guid eventId);
    }
}
