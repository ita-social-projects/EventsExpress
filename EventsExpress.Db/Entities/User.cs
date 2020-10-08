using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public bool IsBlocked { get; set; }

        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public Guid? PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        // navigation properties:
        public virtual IEnumerable<Event> Events { get; set; }

        public virtual IEnumerable<UserEvent> EventsToVisit { get; set; }

        public virtual IEnumerable<UserCategory> Categories { get; set; }

        public virtual IEnumerable<Rate> Rates { get; set; }

        public virtual IEnumerable<Relationship> Relationships { get; set; }

        public virtual IEnumerable<UserChat> Chats { get; set; }

        public virtual ICollection<EventStatusHistory> ChangedStatusEvents { get; set; }
    }
}
