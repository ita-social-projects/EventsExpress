namespace EventsExpress.Test.ValidationTests.TestClasses.Location
{
    using System.Collections;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels.Base;

    public class InCorrectEnumViewModel : IEnumerable
    {
        private readonly MapViewModel firstIncorrectView = new MapViewModel { Latitude = 7.7, Longitude = 8.8, OnlineMeeting = "https://example.com/", Type = (LocationType)8 };
        private readonly MapViewModel secondIncorrectView = new MapViewModel { Latitude = 7.7, Longitude = 8.8, OnlineMeeting = "https://example.com/", Type = (LocationType)9 };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { firstIncorrectView };
            yield return new object[] { secondIncorrectView };
        }
    }
}
