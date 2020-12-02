using System;
using System.Collections.Generic;

namespace EventsExpress.ViewModels
{
    public class EventPreviewViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int MaxParticipants { get; set; }

        public string PhotoUrl { get; set; }

        public IEnumerable<UserPreviewDto> Owners { get; set; }

        public CountryViewModel Country { get; set; }

        public CityViewModel City { get; set; }

        public bool IsBlocked { get; set; }

        public int CountVisitor { get; set; }

        public bool IsPublic { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
