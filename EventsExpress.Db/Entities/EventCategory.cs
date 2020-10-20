using System;

namespace EventsExpress.Db.Entities
{
    public class EventCategory
    {
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
