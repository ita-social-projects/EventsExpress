using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class CreateEventVerificationMessage : INotification
    {
        public CreateEventVerificationMessage(OccurenceEventDTO eventDto)
        {
            OccurenceEvent = eventDto;
        }

        public OccurenceEventDTO OccurenceEvent { get; }
    }
}
