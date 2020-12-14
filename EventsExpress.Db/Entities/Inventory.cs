using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities
{
    public class Inventory : BaseEntity
    {
        [Track]
        public double NeedQuantity { get; set; }

        [Track]
        public string ItemName { get; set; }

        [Track]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        [Track]
        public Guid UnitOfMeasuringId { get; set; }

        public virtual UnitOfMeasuring UnitOfMeasuring { get; set; }

        public virtual ICollection<UserEventInventory> UserEventInventories { get; set; }
    }
}
