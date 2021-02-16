namespace EventsExpress.Db.Entities
{
    using System.Collections.Generic;
    using EventsExpress.Db.Enums;

    public class NotificationType
    {
        public NotificationChange Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<UserNotificationType> Users { get; set; }
    }
}
