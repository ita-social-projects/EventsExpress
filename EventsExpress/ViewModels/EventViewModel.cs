using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class EventViewModel
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

        public Guid PhotoId { get; set; }

        public string PhotoUrl { get; set; }

        public UserPreviewViewModel User { get; set; }

        public CityViewModel City { get; set; }

        public CountryViewModel Country { get; set; }

        public bool IsPublic { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<UserPreviewViewModel> Visitors { get; set; }

        public IEnumerable<InventoryViewModel> Inventories { get; set; }

        public IEnumerable<UserPreviewViewModel> Owners { get; set; }
    }
}
