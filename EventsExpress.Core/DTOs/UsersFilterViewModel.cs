namespace EventsExpress.Core.DTOs
{
    public class UsersFilterViewModel
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 12;

        public string KeyWord { get; set; }

        public string Role { get; set; }

        public bool Blocked { get; set; }

        public bool UnBlocked { get; set; }

        public bool? IsConfirmed { get; set; } = null;

        public SortBy SortBy { get; set; }
    }
}
