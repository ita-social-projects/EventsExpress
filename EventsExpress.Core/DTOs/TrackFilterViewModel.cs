using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class TrackFilterViewModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<string> EntityName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public IEnumerable<ChangesType> ChangesType { get; set; }
    }
}
