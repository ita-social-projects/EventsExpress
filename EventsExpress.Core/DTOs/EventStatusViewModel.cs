using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class EventStatusViewModel
    {
        public EventStatus EventFilterStatus { get; set; } = EventStatus.Active;

        public bool Checked { get; set; } = true;
    }
}
