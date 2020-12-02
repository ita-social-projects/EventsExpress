using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.Services
{
    public class EventOwnersService : BaseService<EventOwner>, IEventOwnersService
    {
        public EventOwnersService(AppDbContext context)
            : base(context)
        {
        }

        public async Task PromoteToOwner(Guid userId, Guid eventId)
        {
            if (!_context.EventOwners.Any(x => x.EventId == eventId && x.UserId == userId))
            {
                _context.EventOwners.Add(new EventOwner { EventId = eventId, UserId = userId });

                _context.UserEvent.Remove(new UserEvent { UserId = userId, EventId = eventId });

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOwnerFromEvent(Guid userId, Guid eventId)
        {
            _context.EventOwners.Remove(new EventOwner { UserId = userId, EventId = eventId });
            await _context.SaveChangesAsync();
        }
    }
}
