using System;
using System.Collections.Generic;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public bool IsBlocked { get; set; }

        public byte Attitude { get; set; }

        public double Rating { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }

        public IEnumerable<NotificationTypeDto> NotificationTypes { get; set; }

        public IEnumerable<Rate> MyRates { get; set; }
    }
}
