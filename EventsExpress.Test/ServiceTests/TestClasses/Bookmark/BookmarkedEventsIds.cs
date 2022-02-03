using System.Collections;
using EventsExpress.Test.ServiceTests.TestClasses.Event;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.Bookmark;

public class BookmarkedEventsIds : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new TestCaseData(EventTestData.FirstUserId, EventTestData.ThirdEventId);
    }
}
