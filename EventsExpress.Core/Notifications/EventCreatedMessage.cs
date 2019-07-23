using EventsExpress.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Notifications
{
    public class EventCreatedMessage : INotification
    {
        public string Message { get; }
        public EventDTO Event { get; }

        public EventCreatedMessage(EventDTO eventDTO)
        {
            Message = $"new event was Created: id {eventDTO.Id}";
            Event = eventDTO;
        }
    }
}
