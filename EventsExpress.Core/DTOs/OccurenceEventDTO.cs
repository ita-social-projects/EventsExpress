using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class OccurenceEventDTO
    {
        public Guid Id { get; set; }

        public int Frequency { get; set; }

        public Periodicity Periodicity { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public bool IsActive { get; set; }

        public Guid EventId { get; set; }

        public EventDTO Event { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
