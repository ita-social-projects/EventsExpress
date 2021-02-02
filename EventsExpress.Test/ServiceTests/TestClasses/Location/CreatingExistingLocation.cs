namespace EventsExpress.Test.ServiceTests.TestClasses.Location
{
    using System;
    using System.Collections;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Db.Enums;
    using NetTopologySuite.Geometries;

    public class CreatingExistingLocation : IEnumerable
    {
       public static LocationDto LocationDTOMap = new LocationDto
            {
                Id = Guid.NewGuid(),
                Point = new Point(12.45, 24.6),
                Type = LocationType.Map,
            };

       public static LocationDto LocationDTOOnline = new LocationDto
            {
                Id = Guid.NewGuid(),
                OnlineMeeting = new Uri("http://www.example.edu/?ball=box"),
                Type = LocationType.Online,
            };

       public IEnumerator GetEnumerator()
        {
            yield return new object[] { LocationDTOMap };
            yield return new object[] { LocationDTOOnline };
        }
    }
}
