using System;
using System.Collections.Generic;

namespace EventsExpress.DTO
{
    public class InventoryDto
    {
        public Guid Id { get; set; }

        public double NeedQuantity { get; set; }

        public string ItemName { get; set; }

        public UnitOfMeasuringDto UnitOfMeasuring { get; set; }

        public IEnumerable<UserEventInventoryDto> UserEventInventories { get; set; }
    }
}
