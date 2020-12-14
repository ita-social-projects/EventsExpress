using System;
using EventsExpress.Db.EF;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class EventSchedule : ManageableEntity
    {
        public int Frequency { get; set; }

        [Track]
        public DateTime LastRun { get; set; }

        [Track]
        public DateTime NextRun { get; set; }

        public Periodicity Periodicity { get; set; }

        [Track]
        public bool IsActive { get; set; }

        [Track]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
