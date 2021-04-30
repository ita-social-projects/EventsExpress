using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class UserPreviewViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public double Rating { get; set; }

        public Attitude Attitude { get; set; }

        public UserStatusEvent? UserStatusEvent { get; set; }
    }
}
