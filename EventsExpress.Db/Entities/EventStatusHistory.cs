using System;
using EventsExpress.Db.EF;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    [Track]
    public class EventStatusHistory : BaseEntity
    {
        [Track]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Track]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        [Track]
        public EventStatus EventStatus { get; set; } = EventStatus.Active;

        [Track]
        public string Reason { get; set; }

        [Track]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
