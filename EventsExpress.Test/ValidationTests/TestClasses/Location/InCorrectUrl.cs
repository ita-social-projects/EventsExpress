namespace EventsExpress.Test.ValidationTests.TestClasses.Location
{
    using System.Collections;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels.Base;

    public class InCorrectUrl : IEnumerable
    {
        private readonly string[] urlArr = new string[]
                         {
                            "htps://example.com/balance.aspx?ants=board&bag=boat",
                            "htt p://blood.example.net/breath/birthday?army=bell",
                            "://branch.example.com/",
                            "htt://bead.example.com/",
                            null,
                            "        ",
                         };

        private LocationType type = LocationType.Online;

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < urlArr.Length; i++)
            {
                yield return new object[] { new LocationViewModel { Type = type, OnlineMeeting = urlArr[i] } };
            }
        }
    }
}
