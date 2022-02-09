using System;
using System.Collections;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    internal class GetEventExistingId : IEnumerable
    {
        private static Guid[] EventIds => new[]
        {
            EventTestData.FirstEventId,
            EventTestData.SecondEventId,
            EventTestData.ThirdEventId,
            EventTestData.PrivateEventId,
            EventTestData.IsPublicNullEventId,
        };

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < EventIds.Length; i++)
            {
                yield return new TestCaseData(EventIds[i])
                {
                    TestName = $"Case_Id{i}_ExecutesSuccessfully",
                };
            }
        }
    }
}
