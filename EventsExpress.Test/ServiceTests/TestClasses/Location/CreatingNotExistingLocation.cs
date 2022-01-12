namespace EventsExpress.Test.ServiceTests.TestClasses.Location
{
    using System;
    using System.Collections;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Db.Enums;
    using NetTopologySuite.Geometries;

    public class CreatingNotExistingLocation : IEnumerable
    {
        private LocationDto locationDtoPoint = new LocationDto { Id = Guid.NewGuid(), Point = new Point(1.1, 4.5), Type = LocationType.Map };
        private LocationDto locationDtoOnline = new LocationDto { Id = Guid.NewGuid(), OnlineMeeting = "https://example.com/bead", Type = LocationType.Online };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { locationDtoPoint };
            yield return new object[] { locationDtoOnline };
        }
    }
}
