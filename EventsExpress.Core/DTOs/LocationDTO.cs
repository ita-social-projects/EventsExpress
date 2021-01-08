using System;
using System.Collections.Generic;
using System.Text;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.DTOs
{
    public class LocationDTO
    {
        public Guid Id { get; set; }

        public Point Point { get; set; }
    }
}
