namespace EventsExpress.Core.NotificationTemplateModels
{
    public class ParticipationNotificationTemplateModel : INotificationTemplateModel
    {
        public string UserEmail { get; set; }

        public string EventLink { get; set; }
    }
}
