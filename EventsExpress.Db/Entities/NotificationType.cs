namespace EventsExpress.Db.Entities
{
    using System.Collections.Generic;
    using EventsExpress.Db.EF;
    using EventsExpress.Db.Enums;

    [Track]
    public class NotificationType
    {
        public NotificationChange Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<UserNotificationType> Users { get; set; }
    }
}
