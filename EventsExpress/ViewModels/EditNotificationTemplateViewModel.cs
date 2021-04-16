using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class EditNotificationTemplateViewModel
    {
        public NotificationProfile Id { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
