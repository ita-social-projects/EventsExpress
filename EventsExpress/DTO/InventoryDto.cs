using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class InventoryDto
    {
        public Guid Id { get; set; }

        public double NeedQuantity { get; set; }

        public string ItemName { get; set; }
    }
}
