using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.ViewModels
{
    public class UserEventInventoryViewModel
    {
        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public UserPreviewViewModel User { get; set; }

        public Guid InventoryId { get; set; }

        public double Quantity { get; set; }
    }
}
