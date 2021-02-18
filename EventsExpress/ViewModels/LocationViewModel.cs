namespace EventsExpress.ViewModels.Base
{
    using EventsExpress.Db.Enums;

    public class LocationViewModel
    {
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string OnlineMeeting { get; set; }

        public LocationType Type { get; set; }
    }
}
