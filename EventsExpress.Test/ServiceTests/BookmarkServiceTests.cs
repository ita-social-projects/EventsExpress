using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Test.ServiceTests.TestClasses.Bookmark;
using EventsExpress.Test.ServiceTests.TestClasses.Event;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests;

[TestFixture]
public class BookmarkServiceTests : TestInitializer
{
    private BookmarkService _service;
    private Mock<ISecurityContext> _securityContext;

    [SetUp]
    protected override void Initialize()
    {
        base.Initialize();

        Context.Users.AddRange(EventTestData.Users);
        Context.Events.AddRange(EventTestData.Events);
        Context.EventBookmarks.Add(new EventBookmark
        {
            UserFromId = EventTestData.FirstUserId,
            EventId = EventTestData.ThirdEventId,
        });
        Context.SaveChanges();

        _securityContext = new Mock<ISecurityContext>();
        _service = new BookmarkService(Context, _securityContext.Object);
    }

    [TestCaseSource(typeof(NotBookmarkedEventsIds))]
    public async Task SaveEventToBookmarksAsync_WhenValidIdsPassed_ShouldSaveBookmark(
        Guid userId,
        Guid eventId)
    {
        _securityContext.Setup(c => c.GetCurrentUserId()).Returns(userId);
        var expected = Context.EventBookmarks.Count() + 1;

        await _service.SaveEventToBookmarksAsync(eventId);
        var actual = Context.EventBookmarks.Count();

        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(BookmarkedEventsIds))]
    public void SaveEventToBookmarksAsync_WhenBookmarkAlreadyExists_DoesNotThrowException(
        Guid userId,
        Guid eventId)
    {
        _securityContext.Setup(c => c.GetCurrentUserId()).Returns(userId);

        async Task SaveEvent() => await _service.SaveEventToBookmarksAsync(eventId);

        Assert.DoesNotThrowAsync(SaveEvent);
    }

    [TestCaseSource(typeof(BookmarkedEventsIds))]
    public async Task DeleteEventFromBookmarksAsync_WhenBookmarkExists_ShouldDeleteBookmark(
        Guid userId,
        Guid eventId)
    {
        _securityContext.Setup(c => c.GetCurrentUserId()).Returns(userId);
        var expected = Context.EventBookmarks.Count() - 1;

        await _service.DeleteEventFromBookmarksAsync(eventId);
        var actual = Context.EventBookmarks.Count();

        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(NotBookmarkedEventsIds))]
    public void DeleteEventFromBookmarksAsync_WhenBookmarkDoesntExist_DoesNotThrowException(
        Guid userId,
        Guid eventId)
    {
        _securityContext.Setup(c => c.GetCurrentUserId()).Returns(userId);

        async Task DeleteEvent() => await _service.DeleteEventFromBookmarksAsync(eventId);

        Assert.DoesNotThrowAsync(DeleteEvent);
    }
}
