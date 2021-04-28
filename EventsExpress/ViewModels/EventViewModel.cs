using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels.Base;

namespace EventsExpress.ViewModels
{
    public class EventViewModel : EventViewModelBase
    {
        public Guid Id { get; set; }

        public EventStatus EventStatus { get; set; }

        public IEnumerable<UserPreviewViewModel> Visitors { get; set; }

        public IEnumerable<InventoryViewModel> Inventories { get; set; }
    }
}
