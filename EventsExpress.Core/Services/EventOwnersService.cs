using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{

    public class EventOwnersService : BaseService<EventOwner>, IEventOwnersService
    {

        public EventOwnersService(AppDbContext context)
            : base(context)
        {
        }

        public async Task<OperationResult> PromoteToOwner(Guid userId, Guid eventId)
        {
            if (!_context.EventOwners.Any(x => x.EventId == eventId && x.UserId == userId))
            {
                try
                {
                    _context.EventOwners.Add(new EventOwner { EventId = eventId, UserId = userId });

                    _context.UserEvent.Remove(new UserEvent { UserId = userId, EventId = eventId });

                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    return new OperationResult(true, ex.Message, string.Empty);
                }
            }
            return new OperationResult(true);

        }

        public async Task<OperationResult> DeleteOwnerFromEvent(Guid userId, Guid eventId)
        {
            _context.EventOwners.Remove(new EventOwner { UserId = userId, EventId = eventId });
            await _context.SaveChangesAsync();
            return new OperationResult(true);
        }
    }
}
