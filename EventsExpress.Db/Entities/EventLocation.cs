using System;
using EventsExpress.Db.EF;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    [Track]
    public class EventLocation : BaseEntity
    {
        [Track]
        public double Latitude { get; set; }

        [Track]
        public double Longitude { get; set; }

        [Track]
        public string Desc { get; set; }
    }
}
