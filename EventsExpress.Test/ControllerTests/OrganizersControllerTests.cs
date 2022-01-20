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
public class OrganizersControllerTests
{
    private OrganizersController _controller;
    private Mock<IEventOrganizersService> _eventOrganizersService;

    [SetUp]
    protected void Initialize()
    {
        _eventOrganizersService = new Mock<IEventOrganizersService>();
        _eventOrganizersService.Setup(service =>
                                   service.PromoteToOrganizer(It.IsAny<Guid>(), It.IsAny<Guid>()))
                               .Verifiable();

        _eventOrganizersService.Setup(service =>
                                   service.DeleteOrganizerFromEvent(
                                       It.IsAny<Guid>(),
                                       It.IsAny<Guid>()))
                               .Verifiable();

        _controller = new OrganizersController(_eventOrganizersService.Object);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext(),
        };
    }

    [Test]
    public async Task DeleteFromOrganizers_WithValidIds_ShouldReturnOkResult()
    {
        var userId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        var response = await _controller.DeleteFromOrganizers(userId, eventId);

        Assert.IsInstanceOf<OkResult>(response);
        _eventOrganizersService.Verify(
            service => service.DeleteOrganizerFromEvent(It.IsAny<Guid>(), It.IsAny<Guid>()),
            Times.Once);
    }

    [Test]
    public async Task PromoteToOrganizer_WithValidIds_ShouldReturnOkResult()
    {
        var userId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        var response = await _controller.PromoteToOrganizer(userId, eventId);

        Assert.IsInstanceOf<OkResult>(response);
        _eventOrganizersService.Verify(
            service => service.PromoteToOrganizer(It.IsAny<Guid>(), It.IsAny<Guid>()),
            Times.Once);
    }
}
