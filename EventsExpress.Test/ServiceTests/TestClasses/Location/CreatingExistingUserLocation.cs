using System;
using System.Collections;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;

namespace EventsExpress.Test.ServiceTests.TestClasses.Location;

public class CreatingExistingUserLocation : IEnumerable
{
    private static LocationDto locationDTOMap = new LocationDto
    {
        Id = Guid.NewGuid(),
        Point = new Point(13.12, 24.26),
        Type = LocationType.Map,
    };

    public static LocationDto LocationDTOMap
    {
        get => locationDTOMap;
    }

    public IEnumerator GetEnumerator()
    {
        yield return new object[] { LocationDTOMap };
    }
}
