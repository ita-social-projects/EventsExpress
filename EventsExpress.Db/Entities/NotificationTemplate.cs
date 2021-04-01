namespace EventsExpress.Db.Entities
{
    public class NotificationTemplate : BaseEntity
    {
        public string Title { get; set; }

        public string Subject { get; set; }

        public string MessageText { get; set; }
    }
}
