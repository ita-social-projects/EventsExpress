using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class EventStatusHistory : BaseEntity
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        public EventStatus EventStatus { get; set; } = EventStatus.Active;

        public string Reason { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
