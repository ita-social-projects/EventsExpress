using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class ContactAdminFilterViewModel
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 8;

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public SortBy SortBy { get; set; }

        public List<ContactAdminStatus> Status { get; set; }
    }
}
