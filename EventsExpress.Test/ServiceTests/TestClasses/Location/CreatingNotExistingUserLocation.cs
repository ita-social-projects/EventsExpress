using System;
using System.Collections;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;

namespace EventsExpress.Test.ServiceTests.TestClasses.Location;

public class CreatingNotExistingUserLocation : IEnumerable
{
    private LocationDto locationDtoOnline = new LocationDto { Id = Guid.NewGuid(), OnlineMeeting = "https://example.com/bead", Type = LocationType.Online };

    public IEnumerator GetEnumerator()
    {
        yield return new object[] { locationDtoOnline };
    }
}
