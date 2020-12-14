using System;
using System.Collections.Generic;
using EventsExpress.Db.EF;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class UserEvent
    {
        [Track]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Track]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        [Track]
        public Status Status { get; set; }

        public virtual ICollection<UserEventInventory> Inventories { get; set; }

        [Track]
        public UserStatusEvent UserStatusEvent { get; set; }
    }
}
