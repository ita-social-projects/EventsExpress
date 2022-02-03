using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities
{
    [Track]
    public class Rate : EventRelationship
    {
        [Track]
        public byte Score { get; set; }
    }
}
