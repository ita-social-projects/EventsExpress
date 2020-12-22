using System;
using EventsExpress.Db.Enums;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class EventStatusMessage : INotification
    {
        public EventStatusMessage(Guid eventId, string reason, EventStatus eventStatus)
        {
            EventId = eventId;
            Reason = reason;
            EventStatus = eventStatus;
        }

        public Guid EventId { get; set; }

        public string Reason { get; set; }

        public EventStatus EventStatus { get; set; }
    }
}
