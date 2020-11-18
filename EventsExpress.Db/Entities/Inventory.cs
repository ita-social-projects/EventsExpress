using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Inventory : BaseEntity
    {
        public double NeedQuantity { get; set; }

        public string ItemName { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        public Guid UnitOfMeasuringId { get; set; }

        public virtual UnitOfMeasuring UnitOfMeasuring { get; set; }

        public virtual ICollection<UserEventInventory> UserEventInventories { get; set; }
    }
}
