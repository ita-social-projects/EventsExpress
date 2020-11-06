using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class EventStatusHistoryDto
    {
        public Guid EventId { get; set; }

        public string Reason { get; set; }
    }
}
