using System;

namespace EventsExpress.ViewModels
{
    public class AttitudeViewModel
    {
        public Guid UserFromId { get; set; }

        public Guid UserToId { get; set; }

        public byte Attitude { get; set; }
    }
}
