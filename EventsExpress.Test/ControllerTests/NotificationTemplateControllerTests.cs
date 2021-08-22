using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class NotificationTemplateControllerTests : TestInitializer
    {
        private EditNotificationTemplateViewModel _templateViewModel;
        private Mock<INotificationTemplateService> _mockNotificationTemplateService;
        private NotificationTemplateController _controller;

        [OneTimeSetUp]
        protected override void Initialize()
        {
            base.Initialize();
            _templateViewModel = new EditNotificationTemplateViewModel
            {
                Id = NotificationProfile.BlockedUser,
                Subject = "testSubject",
                Message = "testMessage",
            };
            _mockNotificationTemplateService = new Mock<INotificationTemplateService>();

            IEnumerable<NotificationTemplateDto> templates = new List<NotificationTemplateDto>()
            {
                new NotificationTemplateDto { Id = NotificationProfile.BlockedUser, Title = "BlockedUser" },
                new NotificationTemplateDto { Id = NotificationProfile.EventCreated, Title = "EventCreated" },
            };

            MockMapper.Setup(m => m.Map<NotificationTemplateDto>(It.IsAny<EditNotificationTemplateViewModel>()))
                .Returns((EditNotificationTemplateViewModel viewModel) => new NotificationTemplateDto
                {
                    Id = viewModel.Id,
                    Subject = viewModel.Subject,
                    Message = viewModel.Message,
                });

            _mockNotificationTemplateService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(templates);
            _mockNotificationTemplateService.Setup(s => s.GetByIdAsync(It.IsAny<NotificationProfile>()))
                .ReturnsAsync((NotificationProfile id) => new NotificationTemplateDto { Id = id });
            _mockNotificationTemplateService.Setup(s => s.UpdateAsync(It.IsAny<NotificationTemplateDto>()))
                .Returns(Task.CompletedTask);

            _controller = new NotificationTemplateController(_mockNotificationTemplateService.Object, MockMapper.Object);
        }

        [Test]
        public void GetTemplateProperties_Called_GetModelByTemplateId_Method()
        {
            // Arrange
            _mockNotificationTemplateService.Setup(s =>
                    s.GetModelPropertiesByTemplateId(It.IsAny<NotificationProfile>()))
                .Returns(new List<string>());

            // Act
            _controller.GetTemplateProperties(It.IsAny<NotificationProfile>());

            // Assert
            _mockNotificationTemplateService.Verify(
                s => s.GetModelPropertiesByTemplateId(It.IsAny<NotificationProfile>()), Times.Once);
        }

        [Test]
        public void GetTemplateProperties_ReturnsValid()
        {
            // Arrange
            IEnumerable<string> expectedProperties = new List<string> { "Test_Property_0", "Test_Property_1", "Test_Property_2", };

            _mockNotificationTemplateService.Setup(
                    s => s.GetModelPropertiesByTemplateId(It.IsAny<NotificationProfile>()))
                .Returns(expectedProperties);

            // Act
            var actionResult = _controller.GetTemplateProperties(It.IsAny<NotificationProfile>());

            var okResult = actionResult as OkObjectResult;
            var actualProperties = okResult?.Value as IEnumerable<string>;

            // Assert
            CollectionAssert.AreEqual(expectedProperties, actualProperties);
        }

        [Test]
        public async Task GetAll_ReturnsValidType()
        {
            var result = await _controller.GetAll();

            Assert.IsInstanceOf<ActionResult<IEnumerable<NotificationTemplateDto>>>(result);
        }

        [Test]
        public async Task GetAll_ReturnsStatusOK()
        {
            var actionResult = await _controller.GetAll();

            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task GetById_IsValidType()
        {
            var actionResult = await _controller.GetById(NotificationProfile.BlockedUser);
            Assert.IsInstanceOf<ActionResult<NotificationTemplateDto>>(actionResult);
        }

        [Test]
        public async Task GetById_ReturnsValidValue()
        {
            var actionResult = await _controller.GetById(NotificationProfile.BlockedUser);
            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotificationTemplateDto>(result.Value);
            Assert.AreEqual(NotificationProfile.BlockedUser, ((NotificationTemplateDto)result.Value).Id);
        }

        [Test]
        public async Task GetById_ReturnsNotFound()
        {
            _mockNotificationTemplateService.Setup(s => s.GetByIdAsync(It.IsAny<NotificationProfile>()))
                .ReturnsAsync((NotificationTemplateDto)null);

            var actionResult = await _controller.GetById(NotificationProfile.BlockedUser);

            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task Update_ReturnsOkResult()
        {
            var actionResult = await _controller.Update(_templateViewModel);

            Assert.IsInstanceOf<OkResult>(actionResult);
        }

        [Test]
        public async Task Update_MapperIsUsedOnce()
        {
            await _controller.Update(_templateViewModel);

            MockMapper.Verify(
                m => m.Map<NotificationTemplateDto>(It.IsAny<EditNotificationTemplateViewModel>()),
                Times.Once);
        }

        [Test]
        public async Task Update_UpdateMethodOfServiceIsCalled()
        {
            await _controller.Update(_templateViewModel);

            _mockNotificationTemplateService.Verify(
                s => s.UpdateAsync(It.IsAny<NotificationTemplateDto>()),
                Times.Once);
        }
    }
}
