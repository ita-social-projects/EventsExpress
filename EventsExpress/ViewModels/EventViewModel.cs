using System;
using System.Collections.Generic;
using EventsExpress.ViewModels.Base;

namespace EventsExpress.ViewModels
{
    public class EventViewModel : EventViewModelBase
    {
        public Guid Id { get; set; }

        public Guid PhotoId { get; set; }

        public string PhotoUrl { get; set; }

        public IEnumerable<UserPreviewViewModel> Visitors { get; set; }

        public IEnumerable<InventoryViewModel> Inventories { get; set; }
    }
}
