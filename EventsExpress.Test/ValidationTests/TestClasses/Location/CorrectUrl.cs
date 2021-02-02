namespace EventsExpress.Test.ValidationTests.TestClasses.Location
{
    using System.Collections;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels.Base;

    public class CorrectUrl : IEnumerable
    {
        private LocationType type = LocationType.Online;
        private readonly string[] urlArr = new string[]
                         {
                            "https://example.com/balance.aspx?ants=board&bag=boat",
                            "http://blood.example.net/breath/birthday?army=bell",
                            "http://branch.example.com/",
                            "https://bead.example.com/",
                         };

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < urlArr.Length; i++)
            {
                yield return new object[] { new LocationViewModel { Type = type, OnlineMeeting = urlArr[i] } };
            }
        }
    }
}
