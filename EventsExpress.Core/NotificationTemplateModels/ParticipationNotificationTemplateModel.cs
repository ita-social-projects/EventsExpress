namespace EventsExpress.Core.NotificationTemplateModels
{
    public class ParticipationNotificationTemplateModel : INotificationTemplateModel
    {
        public string UserName { get; set; }

        public string EventLink { get; set; }
    }
}
