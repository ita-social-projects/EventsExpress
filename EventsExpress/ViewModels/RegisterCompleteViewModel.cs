using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class RegisterCompleteViewModel
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }

        public Guid AccountId { get; set; }
    }
}
