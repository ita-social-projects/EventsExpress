using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;

namespace EventsExpress.DTO
{
    public class EventScheduleDto
    {
        public Guid Id { get; set; }

        public int Frequency { get; set; }

        public Periodicity Periodicity { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public bool IsActive { get; set; }

        public EventDTO Event { get; set; }

        public Guid EventId { get; set; }
    }
}
