namespace EventsExpress.Test.ServiceTests.TestClasses.Location
{
    using System;
    using System.Collections;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Db.Enums;
    using NetTopologySuite.Geometries;

    public class CreatingExistingLocation : IEnumerable
    {
        private static LocationDto locationDTOMap = new LocationDto
            {
                Id = Guid.NewGuid(),
                Point = new Point(12.45, 24.6),
                Type = LocationType.Map,
            };

        private static LocationDto locationDTOOnline = new LocationDto
            {
                Id = Guid.NewGuid(),
                OnlineMeeting = new Uri("http://www.example.edu/?ball=box"),
                Type = LocationType.Online,
            };

        public static LocationDto LocationDTOMap
        {
            get => locationDTOMap;
        }

        public static LocationDto LocationDTOOnline
        {
            get => locationDTOOnline;
        }

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { locationDTOMap };
            yield return new object[] { locationDTOOnline };
        }
    }
}
