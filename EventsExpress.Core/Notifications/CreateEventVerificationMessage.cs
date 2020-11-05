using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class CreateEventVerificationMessage : INotification
    {
        public CreateEventVerificationMessage(OccurenceEventDTO eventDto)
        {
            Event = eventDto;
        }

        public OccurenceEventDTO Event { get; }
    }
}
