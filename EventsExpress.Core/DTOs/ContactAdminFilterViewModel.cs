using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class ContactAdminFilterViewModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public DateTime DateCreated { get; set; }

        public SortBy SortBy { get; set; }

        public ContactAdminStatus Status { get; set; }
    }
}
