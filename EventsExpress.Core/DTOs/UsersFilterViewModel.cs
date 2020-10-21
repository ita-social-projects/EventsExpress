namespace EventsExpress.Core.DTOs
{
    public class UsersFilterViewModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string KeyWord { get; set; }

        public string Role { get; set; }

        public bool Blocked { get; set; }

        public bool UnBlocked { get; set; }

        public bool? IsConfirmed { get; set; } = null;

        public SortBy SortBy { get; set; }
    }
}
