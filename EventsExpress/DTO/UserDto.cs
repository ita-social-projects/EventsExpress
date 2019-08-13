using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.DTO;
using System;
using System.Collections.Generic;

namespace EventsExpress.Core.DTOs
{
    public class UserDto
    {
        public Guid Id;
        public string Name;
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public bool IsBlocked { get; set; }

        public virtual Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public string UserPhoto { get; set; }

        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<UserEvent> EventsToVisit { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }

        public IEnumerable<Rate> MyRates { get; set; }

    }
}
