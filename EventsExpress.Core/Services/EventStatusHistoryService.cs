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
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.Services
{
    public class EventStatusHistoryService : BaseService<EventStatusHistory>, IEventStatusHistoryService
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;

        public EventStatusHistoryService(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor,
            IAuthService authService,
            AppDbContext context)
             : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
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
            record.EventStatus = status;
            record.Reason = reason;
            record.UserId = _authService.GetCurrentUser(_httpContextAccessor.HttpContext.User).Id;

            return record;
        }

        public EventStatusHistory GetLastRecord(Guid eventId, EventStatus status)
        {
            return GetLastRecord(eventId, status);
        }
    }
}
