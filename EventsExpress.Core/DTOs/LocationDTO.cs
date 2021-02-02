using System;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }

        public Point Point { get; set; }

        public Uri OnlineMeeting { get; set; }

        public LocationType Type { get; set; }
    }
}
