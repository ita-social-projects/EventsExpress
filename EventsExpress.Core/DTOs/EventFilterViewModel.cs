using System;

namespace EventsExpress.Core
{
    public enum SortBy
    {
        AscDate,
        DescDate,
        AscRate,
        DescRate,
    }

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
