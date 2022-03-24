using System;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests;

[TestFixture]
internal class UserMoreInfoControllerTests
{
    private Mock<IUserMoreInfoService> service;
    private UserMoreInfoController userMoreInfoController;

    private Mock<IMapper> MockMapper { get; set; }

    [SetUp]
    protected void Initialize()
    {
        MockMapper = new Mock<IMapper>();
        service = new Mock<IUserMoreInfoService>();
        userMoreInfoController = new UserMoreInfoController(service.Object, MockMapper.Object);
        userMoreInfoController.ControllerContext = new ControllerContext();
        userMoreInfoController.ControllerContext.HttpContext = new DefaultHttpContext();
    }

    [Test]
    public void Create_ValidModel_OkResult()
    {
        var userMoreInfoViewModel = new UserMoreInfoCreateViewModel
        {
            ParentStatus = ParentStatus.Kids,
            EventTypes = new[] { EventTypes.Free, EventTypes.Online },
            RelationshipStatus = RelationShipStatus.InARelationship,
            ReasonsForUsingTheSite = new[] { InterestReasons.BeMoreActive },
            LeisureType = TheTypeOfLeisure.Active,
            AdditionalInfo = "AdditionalInfoAboutUser",
        };

        var expected = userMoreInfoController.Create(userMoreInfoViewModel);

        Assert.IsInstanceOf<OkResult>(expected.Result);
    }
}
