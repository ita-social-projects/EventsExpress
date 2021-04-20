using System;
using System.Collections;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    internal static class GetEventExistingId
    {
        private static Guid firstEventId = Guid.NewGuid();
        private static Guid secondEventId = Guid.NewGuid();
        private static Guid thirdEventId = Guid.NewGuid();

        internal static Guid FirstEventId
        {
            get => firstEventId;
        }

        internal static Guid SecondEventId
        {
            get => secondEventId;
        }

        internal static Guid ThirdEventId
        {
            get => thirdEventId;
        }

        internal static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(FirstEventId)
                    .SetName("TestCaseWithFirstEventId");
                yield return new TestCaseData(SecondEventId)
                    .SetName("TestCaseWithSecondEventId");
                yield return new TestCaseData(ThirdEventId)
                    .SetName("TestCaseWithThirdEventId");
            }
        }
    }
}
