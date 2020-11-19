using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class UserEvent
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<UserEventInventory> Inventories { get; set; }

        public UserStatusEvent UserStatusEvent { get; set; }
    }
}
