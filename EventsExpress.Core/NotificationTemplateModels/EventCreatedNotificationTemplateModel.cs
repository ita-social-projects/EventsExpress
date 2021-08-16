namespace EventsExpress.Core.NotificationTemplateModels
{
    public class EventCreatedNotificationTemplateModel : INotificationTemplateModel
    {
        public string UserEmail { get; set; }

        public string EventLink { get; set; }
    }
}
