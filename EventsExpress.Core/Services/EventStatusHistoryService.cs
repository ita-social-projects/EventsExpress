using System;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
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

        public async Task CancelEvent(Guid eventId, string reason)
        {
            var uEvent = _db.EventRepository.Get(eventId);
            if (uEvent == null)
            {
                throw new EventsExpressException("Invalid event id");
            }

            var record = CreateEventStatusRecord(uEvent, reason, EventStatus.Cancelled);
            _db.EventStatusHistoryRepository.Insert(record);

            await _db.SaveAsync();
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
            return _db.EventStatusHistoryRepository.GetLastRecord(eventId, status);
        }
    }
}
