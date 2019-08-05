using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public enum SortBy
    {
        AscDate,
        DescDate,
        AscRate,
        DescRate,

    }

   public class UsersFilterViewModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string KeyWord { get; set; }

        public string Role { get; set; }

        public bool Blocked { get; set; }

        public bool UnBlocked { get; set; }

        public SortBy SortBy { get; set; }
    }
}
