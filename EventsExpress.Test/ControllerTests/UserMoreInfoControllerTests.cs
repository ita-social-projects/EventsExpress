namespace EventsExpress.Test.ControllerTests
{
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
        public void Create_OkResult()
        {
            var userMoreInfoViewModel = new UserMoreInfoCreateViewModel();
            userMoreInfoViewModel.Id = new Guid("e618879f-966e-4bbb-b8d2-c497586e4405");
            userMoreInfoViewModel.UserId = new Guid("D4C124A7-66F7-42DF-E556-08D983455BD7");
            userMoreInfoViewModel.ParentStatus = ParentStatus.Kids;
            userMoreInfoViewModel.EventTypes = new[] { EventType.Free, EventType.Online };
            userMoreInfoViewModel.RelationShipStatus = RelationShipStatus.InARelationship;
            userMoreInfoViewModel.ReasonsForUsingTheSite = new[] { ReasonsForUsingTheSite.BeMoreActive };
            userMoreInfoViewModel.TheTypeOfLeisure = TheTypeOfLeisure.Active;
            userMoreInfoViewModel.AditionalInfoAboutUser = "AditionalInfoAboutUser";

            var expected = userMoreInfoController.Create(userMoreInfoViewModel);
            Assert.IsInstanceOf<OkResult>(expected.Result);
        }
    }
}
