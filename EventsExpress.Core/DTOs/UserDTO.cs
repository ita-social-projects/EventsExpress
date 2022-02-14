using System;
using System.Collections.Generic;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public double Rating { get; set; }

        public byte Attitude { get; set; }

        public bool CanChangePassword { get; set; }

        public Guid AccountId { get; set; }

        public Account Account { get; set; }

        public IEnumerable<EventDto> Events { get; set; }

        public IEnumerable<UserEvent> EventsToVisit { get; set; }

        public IEnumerable<UserCategory> Categories { get; set; }

        public IEnumerable<Rate> MyRates { get; set; }

        public IEnumerable<UserNotificationType> NotificationTypes { get; set; }

        public IEnumerable<Guid> BookmarkedEvents { get; set; }
    }
}
