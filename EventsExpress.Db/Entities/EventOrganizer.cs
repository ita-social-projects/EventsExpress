using System;
using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities
{
    [Track]
    public class EventOrganizer
    {
        [Track]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Track]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
