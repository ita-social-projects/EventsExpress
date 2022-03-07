using System;
using System.ComponentModel.DataAnnotations.Schema;
using EventsExpress.Db.EF;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;

namespace EventsExpress.Db.Entities
{
    [Track]

    public class Location : BaseEntity
    {
        [Track]
        [Column(TypeName = "geography")]
        public Point Point { get; set; }

        [Track]
        public string OnlineMeeting { get; set; }

        [Track]
        public LocationType? Type { get; set; }
    }
}
