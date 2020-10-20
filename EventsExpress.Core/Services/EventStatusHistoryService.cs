using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class EventStatusHistoryService : IEventStatusHistoryService
    {
        private readonly IUnitOfWork _db;

        public EventStatusHistoryService(
            IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
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
    }
}
