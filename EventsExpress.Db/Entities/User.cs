using EventsExpress.Db.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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

        //navigation properties:
        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<UserEvent> EventsToVisit { get; set; }

        public IEnumerable<UserCategory> Categories { get; set; }
        
        public IEnumerable<Rate> Rates { get; set; }

        public IEnumerable<Relationship> Relationships { get; set; }

        public IEnumerable<UserChat> Chats { get; set; }
        public IEnumerable<RefreshToken> RefreshTokens { get; set; }
    }
}
