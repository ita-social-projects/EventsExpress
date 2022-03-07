using System;
using System.Collections.Generic;
using System.Drawing;
using EventsExpress.Db.Enums;
using Point = NetTopologySuite.Geometries.Point;

namespace EventsExpress.Db.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public Account Account { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public Guid? LocationId { get; set; }

        public IEnumerable<EventOrganizer> Events { get; set; }

        public virtual IEnumerable<UserEvent> EventsToVisit { get; set; }

        public virtual Location Location { get; set; }

        public virtual IEnumerable<UserCategory> Categories { get; set; }

        public virtual IEnumerable<Rate> Rates { get; set; }

        public virtual IEnumerable<Relationship> Relationships { get; set; }

        public virtual IEnumerable<UserChat> Chats { get; set; }

        public virtual ICollection<EventStatusHistory> ChangedStatusEvents { get; set; }

        public virtual IEnumerable<UserNotificationType> NotificationTypes { get; set; }

        public virtual IEnumerable<EventBookmark> EventBookmarks { get; set; }
    }
}
