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
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.Services
{
    public class EventStatusHistoryService : IEventStatusHistoryService
    {
        private readonly IUnitOfWork _db;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;

        public EventStatusHistoryService(
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor,
            IAuthService authService)
        {
            _db = unitOfWork;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
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
            record.EventStatus = status;
            record.Reason = reason;
            record.UserId = _authService.GetCurrentUser(_httpContextAccessor.HttpContext.User).Id;

            return record;
        }

        public EventStatusHistory GetLastRecord(Guid eventId, EventStatus status)
        {
            return _db.EventStatusHistoryRepository.GetLastRecord(eventId, status);
        }
    }
}
