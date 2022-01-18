using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class EventFilterViewModel
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 6;

        public string KeyWord { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Guid? VisitorId { get; set; }

        public Guid? OwnerId { get; set; }

        public double? X { get; set; }

        public double? Y { get; set; }

        public double? Radius { get; set; }

        public bool? IsOnlyForAdults { get; set; }

        public LocationType? LocationType { get; set; }

        public List<string> Categories { get; set; }

        public SortBy SortBy { get; set; }

        public List<EventStatus> Statuses { get; set; }

        public List<Guid> Organizers { get; set; }
    }
}
