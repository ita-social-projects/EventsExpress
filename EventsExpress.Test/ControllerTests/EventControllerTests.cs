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
using EventsExpress.ViewModels;
using FluentValidation;
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
        private Mock<IPhotoService> mockPhotoservice;
        private Mock<IValidator<IFormFile>> mockValidator;
        private UserDto _userDto;
        private Guid _idUser = Guid.NewGuid();
        private string _userEmal = "user@gmail.com";

        public PhotoService PhotoService { get; set; }

        private Mock<IMapper> MockMapper { get; set; }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            mockSecurityContextService = new Mock<ISecurityContext>();
            mockPhotoservice = new Mock<IPhotoService>();
            service = new Mock<IEventService>();
            mockValidator = new Mock<IValidator<IFormFile>>();
            eventController = new EventController(service.Object, MockMapper.Object, mockSecurityContextService.Object, mockPhotoservice.Object);
            eventController.ControllerContext = new ControllerContext();
            eventController.ControllerContext.HttpContext = new DefaultHttpContext();

            var mockBlobServiceClient = new Mock<BlobServiceClient>();
            var mockBlobContainer = new Mock<BlobContainerClient>();
            Mock<BlobClient> blobClientMock = new Mock<BlobClient>();
            blobClientMock = new Mock<BlobClient>();

            var mockOpt = new Mock<IOptions<ImageOptionsModel>>();
            mockOpt.Setup(opt => opt.Value.Thumbnail).Returns(400);
            mockOpt.Setup(opt => opt.Value.Image).Returns(1200);

            mockBlobServiceClient
                .Setup(s => s.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(mockBlobContainer.Object);

            mockBlobContainer
                .Setup(c => c.GetBlobClient(It.IsAny<string>()))
                .Returns(blobClientMock.Object);

            PhotoService = new PhotoService(mockOpt.Object, mockBlobServiceClient.Object);

            _userDto = new UserDto
            {
                Id = _idUser,
                Email = _userEmal,
            };
        }

        [Test]
        [TestCase(@"./Images/valid-image.jpg")]
        public void SetEventTempPhoto_ValidPhoto(string testFilePath)
        {
            byte[] bytes = File.ReadAllBytes(testFilePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(testFilePath);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            var file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream",
            };
            Guid id = Guid.NewGuid();

            Assert.DoesNotThrowAsync(async () => await PhotoService.AddEventTempPhoto(file, id));
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
