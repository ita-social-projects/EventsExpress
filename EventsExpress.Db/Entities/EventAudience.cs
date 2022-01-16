using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities
{
    [Track]
    public class EventAudience : BaseEntity
    {
        [Track]
        public bool IsOnlyForAdults { get; set; }
    }
}
