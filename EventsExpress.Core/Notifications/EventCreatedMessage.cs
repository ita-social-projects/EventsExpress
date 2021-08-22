using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class EventCreatedMessage : INotification
    {
        public EventCreatedMessage(EventDto eventDto)
        {
            Event = eventDto;
        }

        public EventDto Event { get; }
   }
}
