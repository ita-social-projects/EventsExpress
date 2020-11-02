using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class OccurenceEvent : ManageableEvent
    {
        public TimeSpan Frequency { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public bool IsActive { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
