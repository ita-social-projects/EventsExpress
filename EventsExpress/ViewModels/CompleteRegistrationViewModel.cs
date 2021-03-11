using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class CompleteRegistrationViewModel // complete registration
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }

        public Guid AccountId { get; set; }
    }
}
