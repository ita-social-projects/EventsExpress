using System;
using System.Threading.Tasks;
using EventsExpress.Controllers;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests;

[TestFixture]
public class BookmarkControllerTests
{
    private BookmarkController _controller;
    private Mock<IBookmarkService> _bookmarkService;

    [SetUp]
    protected void Initialize()
    {
        _bookmarkService = new Mock<IBookmarkService>();
        _bookmarkService.Setup(service => service.SaveEventToBookmarksAsync(It.IsAny<Guid>()))
                        .Verifiable();

        _bookmarkService.Setup(service => service.DeleteEventFromBookmarksAsync(It.IsAny<Guid>()))
                        .Verifiable();

        _controller = new BookmarkController(_bookmarkService.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext(),
            },
        };
    }

    [Test]
    public async Task SaveEventBookmark_WithValidIds_ShouldReturnOkResult()
    {
        var eventId = Guid.NewGuid();

        var response = await _controller.SaveEventBookmark(eventId);

        Assert.IsInstanceOf<OkResult>(response);
        _bookmarkService.Verify(
            service => service.SaveEventToBookmarksAsync(It.IsAny<Guid>()),
            Times.Once);
    }

    [Test]
    public async Task DeleteEventBookmark_WithValidIds_ShouldReturnOkResult()
    {
        var eventId = Guid.NewGuid();

        var response = await _controller.DeleteEventBookmark(eventId);

        Assert.IsInstanceOf<OkResult>(response);
        _bookmarkService.Verify(
            service => service.DeleteEventFromBookmarksAsync(It.IsAny<Guid>()),
            Times.Once);
    }
}
