using System;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;

namespace EventsExpress.Core.Services
{
    public class EventStatusHistoryService : BaseService<EventStatusHistory>, IEventStatusHistoryService
    {
        private readonly IMediator _mediator;

        public EventStatusHistoryService(
            AppDbContext context,
            IMediator mediator)
             : base(context)
        {
            _mediator = mediator;
        }

        public async Task CancelEvent(Guid eventId, string reason)
        {
            var uEvent = _context.Events.Find(eventId);
            if (uEvent == null)
            {
                throw new EventsExpressException("Invalid event id");
            }

            var record = CreateEventStatusRecord(uEvent, reason, EventStatus.Cancelled);
            Insert(record);

            await _context.SaveChangesAsync();
            await _mediator.Publish(new CancelEventMessage(eventId));
        }

        private EventStatusHistory CreateEventStatusRecord(Event e, string reason, EventStatus status)
        {
            var record = new EventStatusHistory
            {
                EventId = e.Id,
                UserId = e.OwnerId,
                EventStatus = status,
                Reason = reason,
            };

            return record;
        }

        public EventStatusHistory GetLastRecord(Guid eventId, EventStatus status)
        {
            return GetLastRecord(eventId, status);
        }
    }
}
