using System;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class OwnEventMessage : INotification
    {
        public OwnEventMessage(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}
