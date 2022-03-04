using System;
using System.Collections;
using EventsExpress.Test.ServiceTests.TestClasses.Event;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.Bookmark;

public class NotBookmarkedEventsIds : IEnumerable
{
    private static Guid[] EventIds => new[]
    {
        EventTestData.FirstEventId,
        EventTestData.SecondEventId,
    };

    public IEnumerator GetEnumerator()
    {
        foreach (var eventId in EventIds)
        {
            yield return new TestCaseData(EventTestData.FirstUserId, eventId);
            yield return new TestCaseData(EventTestData.SecondUserId, eventId);
        }
    }
}
