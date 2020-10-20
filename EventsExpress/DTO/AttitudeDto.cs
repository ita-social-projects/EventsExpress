using System;

namespace EventsExpress.DTO
{
    public class AttitudeDto
    {
        public Guid UserFromId { get; set; }

        public Guid UserToId { get; set; }

        public byte Attitude { get; set; }
    }
}
