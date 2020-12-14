using EventsExpress.Db.EF;
using System;

namespace EventsExpress.Db.Entities
{
    public class EventCategory
    {
        [Track]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        [Track]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
