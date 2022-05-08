using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class UserPhotoControllerTests
    {
        private Mock<IUserPhotoService> photoService;
        private Mock<IUserService> userService;
        private UserPhotoController controller;
        private UserDto _userDto;
        private Guid _idUser = Guid.NewGuid();
        private string _userEmail = "user@gmail.com";

        [SetUp]
        protected void Initialize()
        {
            _userDto = new UserDto
            {
                Id = _idUser,
                Email = _userEmail,
            };

            photoService = new Mock<IUserPhotoService>();
            userService = new Mock<IUserService>();

            // photoService
            //   .Setup(p => p.GetPhotoFromAzureBlob(It.IsAny<string>()))
            //   .Returns(Task.FromResult(new byte[] { 0 }));
            photoService
                .Setup(p => p.GetUserPhoto(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new byte[] { 0 }));
            controller = new UserPhotoController(photoService.Object, userService.Object);
        }

        [Test]
        public void GetUserPhoto_ReturnsFile()
        {
            var result = controller.GetUserPhoto(Guid.NewGuid());
            var fileResult = result.Result as FileResult;

            Assert.That(fileResult, Is.TypeOf<FileContentResult>());
        }

        [Test]
        [Category("ChangeAvatar")]
        public async Task ChangeAvatar_CorrectUser_OkObjectResult()
        {
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");

            UserPhotoViewModel photoModel = new UserPhotoViewModel() { Photo = file };

            var res = await controller.ChangeAvatar(_userDto.Id, photoModel);

            Assert.IsInstanceOf<OkObjectResult>(res);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            OkObjectResult okResult = res as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            userService.Verify(user => user.ChangeAvatar(_userDto.Id, It.IsAny<IFormFile>()), Times.Exactly(1));
        }

        [Test]
        public void DeleteAvatar_SuccsesfulUserPhotoDeletion()
        {
            var result = controller.DeleteUserPhoto(_userDto.Id);

            Assert.IsInstanceOf<OkResult>(result.Result);
        }
    }
}
