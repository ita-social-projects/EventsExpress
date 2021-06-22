using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    internal class EventControllerTests
    {
        private Mock<IEventService> service;
        private EventController eventController;
        private Mock<ISecurityContext> mockSecurityContextService;
        private Mock<IPhotoService> mockPhotoservice;
        private UserDto _userDto;
        private Guid _idUser = Guid.NewGuid();
        private string _userEmal = "user@gmail.com";

        private Mock<IMapper> MockMapper { get; set; }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            mockSecurityContextService = new Mock<ISecurityContext>();
            mockPhotoservice = new Mock<IPhotoService>();
            service = new Mock<IEventService>();
            eventController = new EventController(service.Object, MockMapper.Object, mockSecurityContextService.Object, mockPhotoservice.Object);
            eventController.ControllerContext = new ControllerContext();
            eventController.ControllerContext.HttpContext = new DefaultHttpContext();
            _userDto = new UserDto
            {
                Id = _idUser,
                Email = _userEmal,
            };
        }

        [Test]
        public void Create_OkResult()
        {
            var expected = eventController.Create();
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void AllDraft_OkResult()
        {
            int x = 1;
            service.Setup(e => e.GetAllDraftEvents(1, 1, out x)).Returns(new List<EventDto>());
            var expected = eventController.AllDraft();
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }
    }
}
