namespace EventsExpress.Core.NotificationTemplateModels
{
    public class EventChangeNotificationTemplateModel : INotificationTemplateModel
    {
        public string UserEmail { get; set; }

        public string EventLink { get; set; }
    }
}
