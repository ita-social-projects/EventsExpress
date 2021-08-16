namespace EventsExpress.Core.NotificationTemplateModels
{
    public class EventStatusNotificationTemplateModel : INotificationTemplateModel
    {
        public string Title { get; set; }

        public string UserName { get; set; }

        public string EventLink { get; set; }
    }
}
