namespace EventsExpress.Core.NotificationTemplateModels
{
    public class CreateEventVerificationNotificationTemplateModel : INotificationTemplateModel
    {
        public string UserEmail { get; set; }

        public string EventScheduleLink { get; set; }
    }
}
