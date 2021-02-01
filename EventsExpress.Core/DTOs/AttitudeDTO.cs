using System;

namespace EventsExpress.Core.DTOs
{
    public class AttitudeDto
    {
        public Guid UserFromId { get; set; }

        public Guid UserToId { get; set; }

        public byte Attitude { get; set; }
    }
}
