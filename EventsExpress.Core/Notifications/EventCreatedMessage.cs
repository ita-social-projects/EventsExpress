using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class EventCreatedMessage : INotification
    {
        public EventCreatedMessage(EventDTO eventDTO)
        {
            Event = eventDTO;
        }

        public EventDTO Event { get; }
   }
}
