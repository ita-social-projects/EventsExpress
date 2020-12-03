using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class UserEventInventoryDTO
    {
        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public UserDTO User { get; set; }

        //public UserEvent UserEvent { get; set; }

        public Guid InventoryId { get; set; }

        public double Quantity { get; set; }
    }
}
