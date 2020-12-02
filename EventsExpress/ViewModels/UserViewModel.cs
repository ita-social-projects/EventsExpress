using System;

namespace EventsExpress.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string TokenId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhotoUrl { get; set; }
    }
}
