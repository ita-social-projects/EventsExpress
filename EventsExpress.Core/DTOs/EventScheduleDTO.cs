using System;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class EventScheduleDto : BaseEntity
    {
        public int Frequency { get; set; }

        public Periodicity Periodicity { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public bool IsActive { get; set; }

        public Guid EventId { get; set; }

        public EventDto Event { get; set; }
    }
}
