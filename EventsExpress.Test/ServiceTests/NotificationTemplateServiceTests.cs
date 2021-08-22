using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.NotificationTemplateModels;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Test.ServiceTests.TestClasses.NotificationTemplate;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class NotificationTemplateServiceTests : TestInitializer
    {
        private static readonly object[] PerformReplacementTestCases =
        {
            new object[] { null, new NotificationTemplateModel { TestProperty = string.Empty } },
            new object[] { string.Empty, null },
        };

        private NotificationTemplateService _service;
        private NotificationTemplate _notificationTemplate;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            _notificationTemplate = new NotificationTemplate
            {
                Id = NotificationProfile.BlockedUser,
                Title = "testTitle",
                Subject = "testSubject",
                Message = "testMessage",
            };

            MockMapper
                .Setup(m => m.Map<NotificationTemplateDto>(It.IsAny<NotificationTemplate>()))
                .Returns(new NotificationTemplateDto
                {
                    Id = _notificationTemplate.Id,
                });

            Context.NotificationTemplates.Add(_notificationTemplate);

            _service = new NotificationTemplateService(
                Context,
                MockMapper.Object);

            Context.SaveChanges();
        }

        [Test]
        public void GetModelPropertiesByTemplateId_ReturnsValid([Values]NotificationProfile profile)
        {
            // Act
            var result = _service.GetModelPropertiesByTemplateId(profile);

            // Assert
            CollectionAssert.IsNotEmpty(result);
        }

        [Test]
        public void GetModelByTemplateId_DoesNotThrowException([Values]NotificationProfile profile)
        {
            // Arrange
            void ActMethod() => _service.GetModelByTemplateId<INotificationTemplateModel>(profile);

            // Assert
            Assert.DoesNotThrow(ActMethod);
        }

        [Test]
        public async Task GetAllAsync_UsedMapper()
        {
            MockMapper
                .Setup(m => m.
                    Map<IEnumerable<NotificationTemplateDto>>(
                        It.IsAny<IEnumerable<NotificationTemplate>>()))
                .Returns(new List<NotificationTemplateDto>
                {
                    new NotificationTemplateDto { Id = _notificationTemplate.Id },
                });

            var result = (await _service.GetAllAsync()).First();

            MockMapper.Verify(
                mapper =>
                mapper.Map<IEnumerable<NotificationTemplateDto>>(
                It.IsAny<IEnumerable<NotificationTemplate>>()),
                Times.Once);
            Assert.AreEqual(_notificationTemplate.Id, result.Id);
        }

        [Test]
        public async Task GetAllAsync_ReturnsIEnumerable()
        {
            var result = await _service.GetAllAsync();
            Assert.IsInstanceOf<IEnumerable<NotificationTemplateDto>>(result);
        }

        [Test]
        public async Task GetByIdAsync_IsValid()
        {
            var result = await _service.GetByIdAsync(NotificationProfile.BlockedUser);
            Assert.AreEqual(_notificationTemplate.Id, result.Id);
        }

        [Test]
        public void GetByIdAsync_ThrowsException()
        {
            async Task MethodInvoke() => await _service.GetByIdAsync(It.IsAny<NotificationProfile>());
            Assert.ThrowsAsync<EventsExpressException>(MethodInvoke);
        }

        [Test]
        public void PerformReplacement_ReturnsValid()
        {
            string text = string.Empty;
            var model = new NotificationTemplateModel { TestProperty = string.Empty };

            var result = _service.PerformReplacement(text, model);

            Assert.IsInstanceOf<string>(result);
        }

        [TestCaseSource(nameof(PerformReplacementTestCases))]
        public void PerformReplacement_ThrowsArgumentNullExceptionForTextParameter(string text, INotificationTemplateModel model)
        {
            Assert.Throws<ArgumentNullException>(() => _service.PerformReplacement(text, model));
        }

        [Test]
        public async Task UpdateAsync_IsValid()
        {
            MockMapper
                .Setup(m => m.Map(
                    It.IsAny<NotificationTemplateDto>(),
                    It.IsAny<NotificationTemplate>()))
                .Returns((NotificationTemplateDto nDto, NotificationTemplate n) =>
                {
                    n.Message = nDto.Message;
                    return n;
                });

            var templateDto = new NotificationTemplateDto
            {
                Id = _notificationTemplate.Id,
                Message = "UpdatedMessage",
            };

            await _service.UpdateAsync(templateDto);

            var updatedTemplate = await Context.NotificationTemplates.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(templateDto.Id));

            Assert.AreEqual(templateDto.Message, updatedTemplate.Message);
        }

        [TestCase(null)]
        public void UpdateAsync_ThrowsException(NotificationTemplateDto templateDto)
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.UpdateAsync(templateDto));
        }

        [Test]
        public async Task UpdateAsync_UsedMapper()
        {
            MockMapper
                .Setup(m => m.Map(
                    It.IsAny<NotificationTemplateDto>(),
                    It.IsAny<NotificationTemplate>()))
                .Returns(_notificationTemplate);

            await _service.UpdateAsync(new NotificationTemplateDto
            {
                Id = _notificationTemplate.Id,
            });

            MockMapper.Verify(
                mapper => mapper.Map(
                    It.IsAny<NotificationTemplateDto>(),
                    It.IsAny<NotificationTemplate>()),
                Times.Once());
        }
    }
}
