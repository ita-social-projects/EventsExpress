using EventsExpress.Core.DTOs;
using EventsExpress.Core.NotificationModels;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class CreateEventVerificationMessage : INotification
    {
        public CreateEventVerificationMessage(EventScheduleDto eventScheduleDto)
        {
            EventSchedule = eventScheduleDto;
            Model = new CreateEventVerificationNotificationModel();
        }

        public EventScheduleDto EventSchedule { get; }

        public CreateEventVerificationNotificationModel Model { get; }
    }
}
