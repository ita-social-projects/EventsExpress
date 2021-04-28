using System;
using System.IO;
using System.Threading.Tasks;
using Azure;
using EventsExpress.Controllers;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class PhotoControllerTests
    {
        private Mock<IPhotoService> service;
        private PhotoController controller;

        [SetUp]
        protected void Initialize()
        {
            service = new Mock<IPhotoService>();

            controller = new PhotoController(service.Object);
        }

        [Test]
        public void GetPreviewEventPhoto_ReturnsFile()
        {
            service
                .Setup(p => p.GetPhotoFromAzureBlob(It.IsAny<string>()))
                .Returns(Task.FromResult(new byte[] { 1 }));

            IActionResult result = controller.GetPreviewEventPhoto(Guid.NewGuid().ToString()) as IActionResult;

            Is.TypeOf<FileContentResult>();
        }

        [Test]
        public void GetPreviewEventPhoto_ReturnsNull()
        {
            service
                .Setup(p => p.GetPhotoFromAzureBlob(It.IsAny<string>()))
                .Throws(new RequestFailedException("photo not found"));

            IActionResult result = controller.GetPreviewEventPhoto(Guid.NewGuid().ToString()) as IActionResult;

            Is.TypeOf<NotFoundResult>();
        }

        [Test]
        public void GetFullEventPhoto_ReturnsFile()
        {
            service
                .Setup(p => p.GetPhotoFromAzureBlob(It.IsAny<string>()))
                .Returns(Task.FromResult(new byte[] { 1, 2 }));

            IActionResult result = controller.GetFullEventPhoto(Guid.NewGuid().ToString()) as IActionResult;

            Is.TypeOf<FileContentResult>();
        }

        [Test]
        public void GetFullEventPhoto_ReturnsNull()
        {
            service
                .Setup(p => p.GetPhotoFromAzureBlob(It.IsAny<string>()))
                .Throws(new RequestFailedException("photo not found"));

            IActionResult result = controller.GetFullEventPhoto(Guid.NewGuid().ToString()) as IActionResult;

            Is.TypeOf<NotFoundResult>();
        }

        [Test]
        public void GetUserPhoto_ReturnsFile()
        {
            service
                .Setup(p => p.GetPhotoFromAzureBlob(It.IsAny<string>()))
                .Returns(Task.FromResult(new byte[] { 1, 2 }));

            IActionResult result = controller.GetUserPhoto(Guid.NewGuid().ToString()) as IActionResult;

            Is.TypeOf<FileContentResult>();
        }

        [Test]
        public void GetUserPhoto_ReturnsNull()
        {
            service
                .Setup(p => p.GetPhotoFromAzureBlob(It.IsAny<string>()))
                .Throws(new RequestFailedException("photo not found"));

            IActionResult result = controller.GetUserPhoto(Guid.NewGuid().ToString()) as IActionResult;

            Is.TypeOf<NotFoundResult>();
        }
    }
}
