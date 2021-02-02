namespace EventsExpress.Test.ValidationTests.TestClasses.Location
{
    using System;
    using System.Collections;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels.Base;

    public class CorrectEnumViewModel : IEnumerable
    {
       private readonly LocationViewModel modelMap = new LocationViewModel { Latitude = 7.7, Longitude = 8.8, OnlineMeeting = "https://example.com/", Type = LocationType.Map };
       private readonly LocationViewModel modelOnline = new LocationViewModel { Latitude = 7.7, Longitude = 8.8, OnlineMeeting = "https://example.com/", Type = LocationType.Online };

       public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelMap };
            yield return new object[] { modelOnline };
        }
    }
}
