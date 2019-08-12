using EventsExpress.Db.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class AttitudeDTO
    {
        public Guid UserFromId { get; set; }

        public Guid UserToId { get; set; }

        public byte Attitude { get; set; }
    }
}
