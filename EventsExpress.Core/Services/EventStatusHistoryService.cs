using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.IRepo;
using MediatR;

namespace EventsExpress.Core.Services
{
    public class EventStatusHistoryService : IEventStatusHistoryService
    {
        private readonly IUnitOfWork _db;
        private readonly IMediator _mediator;

        public EventStatusHistoryService(
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _db = unitOfWork;
            _mediator = mediator;
        }

        public async Task<OperationResult> CancelEvent(Guid eventId, string reason)
        {
            var uEvent = _db.EventRepository.Get(eventId);
            if (uEvent == null)
            {
                return new OperationResult(false, "Invalid event id", "eventId");
            }

            var record = CreateEventStatusRecord(uEvent, reason, EventStatus.Cancelled);
            _db.EventStatusHistoryRepository.Insert(record);

            await _db.SaveAsync();
            await _mediator.Publish(new CancelEventMessage(eventId));

            return new OperationResult(true);
        }

        private EventStatusHistory CreateEventStatusRecord(Event e, string reason, EventStatus status)
        {
            var record = new EventStatusHistory();
            record.EventId = e.Id;
            // todo change logic for many owners
            // record.UserId = e.OwnerId;
            record.EventStatus = status;
            record.Reason = reason;

            return record;
        }

        public EventStatusHistory GetLastRecord(Guid eventId, EventStatus status)
        {
            return _db.EventStatusHistoryRepository.GetLastRecord(eventId, status);
        }
    }
}
