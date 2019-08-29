using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class RateDto
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public byte Rate { get; set; }
    }
}
