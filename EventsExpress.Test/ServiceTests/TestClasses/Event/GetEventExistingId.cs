using System;
using System.Collections;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    internal static class GetEventExistingId
    {
        private static Guid[] eventIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        internal static Guid FirstEventId
        {
            get => eventIds[0];
        }

        internal static Guid SecondEventId
        {
            get => eventIds[1];
        }

        internal static Guid ThirdEventId
        {
            get => eventIds[2];
        }

        internal static Guid IsPublicFalseEventId
        {
            get => eventIds[3];
        }

        internal static Guid IsPublicNullEventId
        {
            get => eventIds[4];
        }

        internal static IEnumerable TestCasesForGetEvent => GetTestCasesFor("GetEvent");

        internal static IEnumerable TestCasesForAddUserToEvent => GetTestCasesFor("AddUserToEvent");

        private static IEnumerable GetTestCasesFor(string caller)
        {
            for (int i = 0; i < eventIds.Length; i++)
            {
                yield return new TestCaseData(FirstEventId)
                    .SetName($"{caller}_TestCase{i}");
            }
        }
    }
}
