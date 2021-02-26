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
        private Mock<IAuthService> auth;

        private Mock<IMapper> MockMapper { get; set; }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            auth = new Mock<IAuthService>();
            service = new Mock<IEventService>();
            eventController = new EventController(service.Object, auth.Object, MockMapper.Object);
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
            var expected = eventController.AllDraft();
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }
    }
}
