using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class UserEventInventory
    {
        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public UserEvent UserEvent { get; set; }

        public Guid InventoryId { get; set; }

        public Inventory Inventory { get; set; }

        public double Quantity { get; set; }
    }
}
