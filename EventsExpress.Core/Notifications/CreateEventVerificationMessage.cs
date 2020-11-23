using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class CreateEventVerificationMessage : INotification
    {
        public CreateEventVerificationMessage(EventScheduleDTO eventScheduleDto)
        {
            EventSchedule = eventScheduleDto;
        }

        public EventScheduleDTO EventSchedule { get; }
    }
}
