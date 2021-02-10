using System;
using System.Collections.Generic;
using EventsExpress.ViewModels.Base;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ViewModels
{
    public class EventCreateViewModel : EventViewModelBase
    {
        public IFormFile Photo { get; set; }

        public Guid? PhotoId { get; set; }

        public IEnumerable<InventoryViewModel> Inventories { get; set; }
    }
}
