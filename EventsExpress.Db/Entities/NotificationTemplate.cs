using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class NotificationTemplate
    {
        public NotificationProfile Id { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string MessageText { get; set; }
    }
}
