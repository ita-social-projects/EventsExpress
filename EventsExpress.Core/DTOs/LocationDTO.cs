using System;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }

        public Point Point { get; set; }
    }
}
