using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.DTO
{
    public class OccurenceEventDto
    {
        public Guid Id { get; set; }

        public TimeSpan Frequency { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public bool IsActive { get; set; }

        public Guid EventId { get; set; }
    }
}
