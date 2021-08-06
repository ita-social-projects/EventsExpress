using System;
using System.Collections.Generic;
using EventsExpress.Core.NotificationModels;
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
            Model = new EventStatusNotificationModel();
        }

        public Guid EventId { get; set; }

        public IEnumerable<Guid> UserIds { get; }

        public string Reason { get; set; }

        public EventStatus EventStatus { get; set; }

        public EventStatusNotificationModel Model { get; }
    }
}
