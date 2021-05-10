﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
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
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public async Task SetStatusEvent(Guid eventId, string reason, EventStatus eventStatus)
        {
            var uEvent = Context.Events.Find(eventId);
            if (uEvent == null)
            {
                throw new EventsExpressException("Invalid event id");
            }

            var record = CreateEventStatusRecord(uEvent, reason, eventStatus);
            Insert(record);

            await Context.SaveChangesAsync();

            await _mediator.Publish(new EventStatusMessage(eventId, reason, eventStatus));
        }

        private EventStatusHistory CreateEventStatusRecord(Event e, string reason, EventStatus status)
        {
            var record = new EventStatusHistory
            {
                EventId = e.Id,
                UserId = _authService.GetCurrentUserId(_httpContextAccessor.HttpContext.User),
                EventStatus = status,
                Reason = reason,
            };

            return record;
        }

        public EventStatusHistory GetLastRecord(Guid eventId, EventStatus status)
        {
            return Context.EventStatusHistory
                .Where(e => e.EventId == eventId && e.EventStatus == status)
                .LastOrDefault();
        }
    }
}
