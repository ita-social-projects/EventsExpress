using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class EventStatusHistoryViewModel
    {
        public Guid EventId { get; set; }

        public string Reason { get; set; }

        public EventStatus EventStatus { get; set; }
    }
}
