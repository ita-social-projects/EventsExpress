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
    [TestFixture]
    internal class TracksControllerTests
    {
        private Mock<ITrackService> _service;
        private TracksController _tracksController;
        private TrackFilterViewModel _filter;

        private Mock<IMapper> MockMapper { get; set; }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            _service = new Mock<ITrackService>();
            _filter = new TrackFilterViewModel();
            _tracksController = new TracksController(_service.Object, MockMapper.Object);
            _tracksController.ControllerContext = new ControllerContext();
            _tracksController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Test]
        public void All_OkResult()
        {
            int x = 1;
            _service.Setup(e => e.GetAllTracks(_filter, out x)).Returns(new List<TrackDto>());
            var expected = _tracksController.All(_filter);
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }
    }
}
