using System;
using System.Collections.Generic;

namespace EventsExpress.Core.DTOs
{
    public class InventoryDTO
    {
        public Guid Id { get; set; }

        public double NeedQuantity { get; set; }

        public string ItemName { get; set; }

        public UnitOfMeasuringDTO UnitOfMeasuring { get; set; }
    }
}
