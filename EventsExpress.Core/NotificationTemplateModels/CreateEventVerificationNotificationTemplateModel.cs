namespace EventsExpress.Core.NotificationTemplateModels
{
    public class CreateEventVerificationNotificationTemplateModel : INotificationTemplateModel
    {
        public string UserName { get; set; }

        public string EventScheduleLink { get; set; }
    }
}
