using EventsExpress.Db.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class AttitudeDto
    {
        public Guid UserFromId { get; set; }

        public Guid UserToId { get; set; }

        public byte Attitude { get; set; }
    }
}
