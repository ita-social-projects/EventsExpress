using System;

namespace EventsExpress.Core.DTOs
{
    public class EventFilterViewModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string KeyWord { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string Categories { get; set; }

        public SortBy SortBy { get; set; }

        public bool Blocked { get; set; }

        public bool Unblocked { get; set; }
    }
}
