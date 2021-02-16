namespace EventsExpress.Db.Entities
{
    using System;
    using EventsExpress.Db.EF;
    using EventsExpress.Db.Enums;

    public class UserNotificationType
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public NotificationChange NotificationTypeId { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
