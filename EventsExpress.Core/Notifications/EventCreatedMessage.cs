using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class EventCreatedMessage : INotification
    {
        public EventCreatedMessage(EventDto eventDTO)
        {
            Event = eventDTO;
        }

        public EventDto Event { get; }
   }
}
