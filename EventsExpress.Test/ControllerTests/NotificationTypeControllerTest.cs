namespace EventsExpress.Test.ControllerTests
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using EventsExpress.Controllers;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Core.IServices;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    internal class NotificationTypeControllerTest
    {
        private Mock<INotificationTypeService> service;
        private NotificationTypeController controller;

        private NotificationTypeDto firstNotificationTypeDTO;

        private NotificationTypeDto secondNotificationTypeDTO;
        private NotificationTypeDto thirdNotificationTypeDTO;

        private Mock<IMapper> MockMapper { get; set; }

        [SetUp]
        protected void Initialize()
        {
            MockMapper = new Mock<IMapper>();
            service = new Mock<INotificationTypeService>();
            controller = new NotificationTypeController(service.Object, MockMapper.Object);
            firstNotificationTypeDTO = new NotificationTypeDto
            {
                Id = NotificationChange.OwnEvent,
                Name = NotificationChange.OwnEvent.ToString(),
                CountOfUser = 8,
            };
            secondNotificationTypeDTO = new NotificationTypeDto
            {
                Id = NotificationChange.VisitedEvent,
                Name = NotificationChange.VisitedEvent.ToString(),
                CountOfUser = 8,
            };
            thirdNotificationTypeDTO = new NotificationTypeDto
            {
                Id = NotificationChange.Profile,
                Name = NotificationChange.Profile.ToString(),
                CountOfUser = 8,
            };
        }

        [Test]
        public void GetAll_OkResult()
        {
            MockMapper.Setup(u => u.Map<IEnumerable<NotificationTypeDto>, IEnumerable<NotificationTypeViewModel>>(It.IsAny<IEnumerable<NotificationTypeDto>>()))
            .Returns((IEnumerable<NotificationTypeDto> e) => e.Select(item => new NotificationTypeViewModel { Id = item.Id, Name = item.Name, CountOfUser = item.CountOfUser }));
            service.Setup(item => item.GetAllNotificationTypes()).Returns(new NotificationTypeDto[] { firstNotificationTypeDTO, secondNotificationTypeDTO, thirdNotificationTypeDTO });

            var expected = controller.All();

            Assert.IsInstanceOf<OkObjectResult>(expected);
        }
    }
}
