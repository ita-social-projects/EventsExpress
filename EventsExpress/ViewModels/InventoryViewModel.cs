using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.ViewModels
{
    public class InventoryViewModel
    {
        public Guid Id { get; set; }

        public double NeedQuantity { get; set; }

        public string ItemName { get; set; }

        public UnitOfMeasuringViewModel UnitOfMeasuring { get; set; }
    }
}
