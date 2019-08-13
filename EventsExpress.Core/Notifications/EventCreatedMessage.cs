using EventsExpress.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Notifications
{
    public class EventCreatedMessage : INotification
    {
        public EventDTO Event { get; }

        public EventCreatedMessage(EventDTO eventDTO)
        {
            Event = eventDTO;
        }
    }
}
