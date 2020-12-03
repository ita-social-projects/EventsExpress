using System;

namespace EventsExpress.ViewModels
{
    public class RateViewModel
    {
        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public byte Rate { get; set; }
    }
}
