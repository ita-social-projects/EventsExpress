using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class OccurenceEventDTO
    {
        public Guid Id { get; set; }

        public TimeSpan Frequency { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public bool IsActive { get; set; }

        public Guid EventId { get; set; }
    }
}
