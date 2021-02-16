namespace EventsExpress.Db.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EventsExpress.Db.EF;
    using EventsExpress.Db.Enums;

    [Track]
    public class UserNotificationType
    {
        [Track]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Track]
        public NotificationChange NotificationTypeId { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
