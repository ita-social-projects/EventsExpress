using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels.Base
{
    public class EventViewModelBase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool IsReccurent { get; set; }

        public int? MaxParticipants { get; set; }

        public int Frequency { get; set; }

        public Periodicity Periodicity { get; set; }

        public MapViewModel Map { get; set; }

        public string OnlineMeeting { get; set; }

        public LocationType LocationType { get; set; }

        public bool? IsPublic { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<UserPreviewViewModel> Owners { get; set; }

        public IEnumerable<UserPreviewViewModel> Members { get; set; }
    }
}
