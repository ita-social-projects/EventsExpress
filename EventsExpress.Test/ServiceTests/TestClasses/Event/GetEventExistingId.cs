namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    using System;
    using System.Collections;

    public class GetEventExistingId : IEnumerable
    {
        private static Guid firstEventId = Guid.NewGuid();
        private static Guid secondEventId = Guid.NewGuid();
        private static Guid thirdEventId = Guid.NewGuid();

        public static Guid FirstEventId
        {
            get => firstEventId;
        }

        public static Guid SecondEventId
        {
            get => secondEventId;
        }

        public static Guid ThirdEventId
        {
            get => thirdEventId;
        }

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { FirstEventId };
            yield return new object[] { SecondEventId };
            yield return new object[] { ThirdEventId };
        }
    }
}
