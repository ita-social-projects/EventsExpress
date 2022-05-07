using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using EventsExpress.Controllers;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    internal class EventPhotoControllerTests
    {
        private EventPhotoController eventPhotoController;
        private Mock<IEventPhotoService> mockEventPhotoservice;
        private Guid _eventId = Guid.NewGuid();

        public EventPhotoService PhotoService { get; set; }

        [SetUp]
        protected void Initialize()
        {
            mockEventPhotoservice = new Mock<IEventPhotoService>();
            eventPhotoController = new EventPhotoController(mockEventPhotoservice.Object);
            eventPhotoController.ControllerContext = new ControllerContext();
            eventPhotoController.ControllerContext.HttpContext = new DefaultHttpContext();

            var mockBlobServiceClient = new Mock<BlobServiceClient>();
            var mockBlobContainer = new Mock<BlobContainerClient>();
            Mock<BlobClient> blobClientMock = new Mock<BlobClient>();

            var mockOpt = new Mock<IOptions<EventImageOptionsModel>>();
            mockOpt.Setup(opt => opt.Value.Thumbnail).Returns(400);
            mockOpt.Setup(opt => opt.Value.Image).Returns(1200);

            mockBlobServiceClient
                .Setup(s => s.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(mockBlobContainer.Object);

            mockBlobContainer
                .Setup(c => c.GetBlobClient(It.IsAny<string>()))
                .Returns(blobClientMock.Object);

            PhotoService = new EventPhotoService(mockOpt.Object, mockBlobServiceClient.Object);
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
        public void SetEventTempPhoto_OkResult()
        {
            var expected = eventPhotoController.SetEventTempPhoto(_eventId, new EventPhotoViewModel());
            Assert.IsInstanceOf<OkResult>(expected.Result);
        }

        [Test]
        public void GetPreviewEventPhoto_ReturnsFile()
        {
            var result = eventPhotoController.GetPreviewEventPhoto(Guid.NewGuid());
            var fileResult = result.Result as FileResult;

            Assert.That(fileResult, Is.TypeOf<FileContentResult>());
        }

        [Test]
        public void GetFullEventPhoto_ReturnsFile()
        {
            var result = eventPhotoController.GetFullEventPhoto(Guid.NewGuid());
            var fileResult = result.Result as FileResult;

            Assert.That(fileResult, Is.TypeOf<FileContentResult>());
        }
    }
}
