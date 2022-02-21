using System;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class JoinedEventMessage : INotification
    {
        public JoinedEventMessage(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}
