using EventsExpress.Db.EF;
using System;

namespace EventsExpress.Db.Entities
{
    [Track]
    public class Rate : BaseEntity
    {
        [Track]
        public Guid UserFromId { get; set; }

        public virtual User UserFrom { get; set; }

        [Track]
        public Guid EventId { get; set; }

        public Event Event { get; set; }

        [Track]
        public byte Score { get; set; }
    }
}
