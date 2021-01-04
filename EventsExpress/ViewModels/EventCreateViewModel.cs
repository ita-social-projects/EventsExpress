using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ViewModels
{
    public class EventCreateViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public bool IsReccurent { get; set; }

        public int MaxParticipants { get; set; }

        public int Frequency { get; set; }

        public Periodicity Periodicity { get; set; }

        public IFormFile Photo { get; set; }

        public Guid PhotoId { get; set; }

        public bool IsPublic { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public IEnumerable<UserPreviewViewModel> Owners { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<InventoryViewModel> Inventories { get; set; }
    }
}
