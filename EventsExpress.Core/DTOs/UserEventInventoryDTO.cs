using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.DTOs
{
    [Track]
    public class UserEventInventoryDTO
    {
        [Track]
        public Guid EventId { get; set; }

        [Track]
        public Guid UserId { get; set; }

        public UserDTO User { get; set; }

        [Track]
        public Guid InventoryId { get; set; }

        [Track]
        public double Quantity { get; set; }
    }
}
