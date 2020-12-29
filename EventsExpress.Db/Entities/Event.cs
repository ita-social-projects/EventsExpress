using System;
using System.Collections.Generic;
using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities
{
    [Track]
    public class Event : BaseEntity
    {
        [Track]
        public bool IsBlocked { get; set; }

        [Track]
        public string Title { get; set; }

        [Track]
        public string Description { get; set; }

        [Track]
        public DateTime DateFrom { get; set; }

        [Track]
        public DateTime DateTo { get; set; }

        [Track]
        public bool IsPublic { get; set; }

        [Track]
        public Guid CityId { get; set; }

        public virtual City City { get; set; }

        [Track]
        public int MaxParticipants { get; set; }

        [Track]
        public Guid? PhotoId { get; set; }

        [Track]
        public Guid? EventLocationId { get; set; }

        public virtual EventSchedule EventSchedule { get; set; }

        public virtual Photo Photo { get; set; }

        public virtual EventLocation EventLocation { get; set; }

        public virtual ICollection<EventOwner> Owners { get; set; }

        public virtual ICollection<UserEvent> Visitors { get; set; }

        public virtual ICollection<EventCategory> Categories { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        public virtual ICollection<EventStatusHistory> StatusHistory { get; set; }
    }
}
