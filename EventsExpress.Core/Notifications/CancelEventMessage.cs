using System;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class CancelEventMessage : INotification
    {
        public CancelEventMessage(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}
