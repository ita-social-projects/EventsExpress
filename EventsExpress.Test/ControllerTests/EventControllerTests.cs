using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ExtensionMethods;
using EventsExpress.ViewModels;
using EventsExpress.ViewModels.Base;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private Mock<IValidator<IFormFile>> mockValidator;
        private Mock<IValidator<EventViewModel>> mockEventViewModelValidator;
        private UserDto _userDto;
        private Guid _idUser = Guid.NewGuid();
        private Guid _eventId = Guid.NewGuid();
        private string _userEmal = "user@gmail.com";

        private Mock<IMapper> MockMapper { get; set; }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            mockSecurityContextService = new Mock<ISecurityContext>();
            service = new Mock<IEventService>();
            mockValidator = new Mock<IValidator<IFormFile>>();
            mockEventViewModelValidator = new Mock<IValidator<EventViewModel>>();
            eventController = new EventController(service.Object, MockMapper.Object, mockSecurityContextService.Object);
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

        [Test]
        public void Upcoming_OkResult()
        {
            int x = 1;
            service.Setup(e => e.GetAll(new EventFilterViewModel(), out x)).Returns(new List<EventDto>());
            var expected = eventController.Upcoming();
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void Upcoming_ReturnBadRequest()
        {
            int count = 1;
            service.Setup(e => e.GetAll(It.IsAny<EventFilterViewModel>(), out count)).Throws<ArgumentOutOfRangeException>();
            var expected = eventController.Upcoming();
            Assert.IsInstanceOf<BadRequestResult>(expected);
        }

        [Test]
        public void AllEvents_OkResult()
        {
            int x = 1;
            service.Setup(e => e.GetAll(new EventFilterViewModel(), out x)).Returns(new List<EventDto>());
            var expected = eventController.All(new EventFilterViewModel());
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void PastEvents_OkResult()
        {
            service.Setup(e => e.PastEventsByUserId(_idUser, new PaginationViewModel() { PageSize = 3, Page = 1 })).Returns(new List<EventDto>());
            var expected = eventController.PastEvents(_idUser);
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void AddUserToEvent_OkResult()
        {
            var expected = eventController.AddUserToEvent(_eventId, _idUser);
            Assert.IsInstanceOf<OkResult>(expected.Result);
        }

        [Test]
        public void GetCurrentRate_OkResult()
        {
            service.Setup(e => e.Exists(_eventId)).Returns(true);
            var expected = eventController.GetCurrentRate(_eventId);
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void CreateNextFromParentWithEdit_OkResult()
        {
            service.Setup(e => e.EditNextEvent(new EventDto())).Returns(Task.FromResult(Guid.NewGuid()));
            var expected = eventController.CreateNextFromParentWithEdit(_eventId, new EventEditViewModel());
            Assert.IsInstanceOf<OkObjectResult>(expected.Result);
        }

        [Test]
        public void Edit_OkResult()
        {
            service.Setup(e => e.Edit(new EventDto())).Returns(Task.FromResult(Guid.NewGuid()));
            var expected = eventController.Edit(_eventId, new EventEditViewModel());
            Assert.IsInstanceOf<OkObjectResult>(expected.Result);
        }

        [Test]
        public void VisitedEvents_OkResult()
        {
            var expected = eventController.VisitedEvents(_idUser, 1);
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        public void Publish_OkResult()
        {
            mockEventViewModelValidator.Setup(v => v.Validate(It.IsAny<EventViewModel>()))
                .Returns(new ValidationResult() { });
            MockMapper.Setup(m => m.Map<EventViewModel>(It.IsAny<EventDto>())).Returns(new EventViewModel() { });
            service.Setup(e => e.EventById(_eventId)).Returns(new EventDto() { });
            service.Setup(e => e.Publish(_eventId)).Returns(Task.FromResult(Guid.NewGuid()));
            var expected = eventController.Publish(_eventId, mockEventViewModelValidator.Object);
            Assert.IsInstanceOf<OkObjectResult>(expected.Result);
        }
    }
}
