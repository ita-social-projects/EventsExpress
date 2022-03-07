namespace EventsExpress.Test.ValidatorTests.TestClasses.Location
{
    using System;
    using System.Collections;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels.Base;

    public class CorrectLocation : IEnumerable
    {
        private readonly Location modelMap = new Location { Point = null, Type = LocationType.Online, OnlineMeeting = null };

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { modelMap };
        }
    }
}
