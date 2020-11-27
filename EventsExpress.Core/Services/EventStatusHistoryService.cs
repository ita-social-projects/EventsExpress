using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        public async Task<OperationResult> CancelEvent(Guid eventId, string reason)
        {
            var uEvent = _context.Events.Find(eventId);
            if (uEvent == null)
            {
                return new OperationResult(false, "Invalid event id", "eventId");
            }

            var record = CreateEventStatusRecord(uEvent, reason, EventStatus.Cancelled);
            Insert(record);

            await _context.SaveChangesAsync();
            await _mediator.Publish(new CancelEventMessage(eventId));

            return new OperationResult(true);
        }

        private EventStatusHistory CreateEventStatusRecord(Event e, string reason, EventStatus status)
        {
            var record = new EventStatusHistory();
            record.EventId = e.Id;
            record.UserId = e.OwnerId;
            record.EventStatus = status;
            record.Reason = reason;

            return record;
        }

        public EventStatusHistory GetLastRecord(Guid eventId, EventStatus status)
        {
            return GetLastRecord(eventId, status);
        }
    }
}
