namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    using System;
    using System.Collections;

    public class GetEventExistingId : IEnumerable
    {
        private static Guid firstEventId = Guid.NewGuid();
        private static Guid secondEventId = Guid.NewGuid();

        public static Guid FirstEventId
        {
            get => firstEventId;
        }

        public static Guid SecondEventId
        {
            get => secondEventId;
        }

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { FirstEventId };
            yield return new object[] { SecondEventId };
        }
    }
}
