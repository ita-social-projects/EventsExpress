using System;
using System.Collections.Generic;
using EventsExpress.Db.EF;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
   [Track]
   public class MultiEventStatus
    {
        [Track]
        public Guid Id { get; set; }

        [Track]
        public Guid ParentId { get; set; }

        public virtual Event ChildEvent { get; set; }

        public virtual Event ParentEvent { get; set; }

        [Track]
        public Guid? ChildId { get; set; }
    }
}
