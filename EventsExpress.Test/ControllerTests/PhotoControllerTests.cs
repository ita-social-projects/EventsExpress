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

            service
                .Setup(p => p.GetPhotoFromAzureBlob(It.IsAny<string>()))
                .Returns(Task.FromResult(new byte[] { 0 }));

            controller = new PhotoController(service.Object);
        }

        [Test]
        public void GetPreviewEventPhoto_ReturnsFile()
        {
            var result = controller.GetPreviewEventPhoto(Guid.NewGuid().ToString());
            var fileResult = result.Result as FileResult;

            Assert.That(fileResult, Is.TypeOf<FileContentResult>());
        }

        [Test]
        public void GetFullEventPhoto_ReturnsFile()
        {
            var result = controller.GetFullEventPhoto(Guid.NewGuid().ToString());
            var fileResult = result.Result as FileResult;

            Assert.That(fileResult, Is.TypeOf<FileContentResult>());
        }

        [Test]
        public void GetUserPhoto_ReturnsFile()
        {
            var result = controller.GetUserPhoto(Guid.NewGuid().ToString());
            var fileResult = result.Result as FileResult;

            Assert.That(fileResult, Is.TypeOf<FileContentResult>());
        }
    }
}
