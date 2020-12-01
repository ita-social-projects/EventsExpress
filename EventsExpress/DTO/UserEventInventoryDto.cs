using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class UserEventInventoryDto
    {
        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public Guid InventoryId { get; set; }

        public double Quantity { get; set; }
    }
}
