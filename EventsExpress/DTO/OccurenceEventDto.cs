using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.DTO
{
    public class OccurenceEventDto
    {
        public Guid Id { get; set; }

        public int Frequency { get; set; }

        public Periodicity Periodicity { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public bool IsActive { get; set; }

        public Guid EventId { get; set; }
    }
}
