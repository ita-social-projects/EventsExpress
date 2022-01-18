using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.Services
{
    public class EventOrganizersService : BaseService<EventOrganizer>, IEventOrganizersService
    {
        public EventOrganizersService(AppDbContext context)
            : base(context)
        {
        }

        public async Task PromoteToOrganizer(Guid userId, Guid eventId)
        {
            if (!Context.EventOwners.Any(x => x.EventId == eventId && x.UserId == userId))
            {
                Context.EventOwners.Add(new EventOrganizer { EventId = eventId, UserId = userId });

                Context.UserEvent.Remove(new UserEvent { UserId = userId, EventId = eventId });

                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrganizerFromEvent(Guid userId, Guid eventId)
        {
            Context.EventOwners.Remove(new EventOrganizer { UserId = userId, EventId = eventId });
            await Context.SaveChangesAsync();
        }
    }
}
