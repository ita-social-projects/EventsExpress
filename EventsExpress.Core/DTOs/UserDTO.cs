using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
  public  class UserDTO
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
        public virtual Guid? PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

        public IEnumerable<EventDTO> Events { get; set; }

        public IEnumerable<UserEvent> EventsToVisit { get; set; }

        public IEnumerable<UserCategory> Categories { get; set; }

        public IEnumerable<Rate> MyRates { get; set; }

    }
}
