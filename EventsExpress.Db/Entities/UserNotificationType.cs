using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class UserNotificationType
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public NotificationChange NotificationTypeId { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
