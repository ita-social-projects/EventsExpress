using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class CreateEventVerificationMessage : INotification
    {
        public CreateEventVerificationMessage(EventScheduleDto eventScheduleDto)
        {
            EventSchedule = eventScheduleDto;
        }

        public EventScheduleDto EventSchedule { get; }
    }
}
