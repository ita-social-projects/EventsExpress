namespace EventsExpress.Db.Entities
{
    public class EmailMessage : BaseEntity
    {
        public string NotificationType { get; set; }

        public string Subject { get; set; }

        public string MessageText { get; set; }
    }
}
