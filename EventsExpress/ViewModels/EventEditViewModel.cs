using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels.Base;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ViewModels
{
    public class EventEditViewModel : EventViewModelBase
    {
        public Guid Id { get; set; }

        public IFormFile Photo { get; set; }

        public Guid PhotoId { get; set; }

        public string PhotoUrl { get; set; }

        public EventStatus EventStatus { get; set; }

        public IEnumerable<InventoryViewModel> Inventories { get; set; }
    }
}
