using EventsExpress.Core.DTOs;
using EventsExpress.Core.NotificationModels;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class EventCreatedMessage : INotification
    {
        public EventCreatedMessage(EventDto eventDTO)
        {
            Event = eventDTO;
            Model = new EventCreatedNotificationModel();
        }

        public EventDto Event { get; }

        public EventCreatedNotificationModel Model { get; }
   }
}
