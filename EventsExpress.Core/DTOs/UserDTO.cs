using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Google.Apis;

namespace EventsExpress.Core.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public bool IsBlocked { get; set; }

        public virtual Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Guid? PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        public double Rating { get; set; }

        public byte Attitude { get; set; }

        public bool CanChangePassword { get; set; }

        public IEnumerable<EventDto> Events { get; set; }

        public IEnumerable<UserEvent> EventsToVisit { get; set; }

        public IEnumerable<UserCategory> Categories { get; set; }

        public IEnumerable<Rate> MyRates { get; set; }

        public IEnumerable<RefreshToken> RefreshTokens { get; set; }
    }
}
