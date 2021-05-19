using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class UsersServiceTests : TestInitializer
    {
        private static Mock<IPhotoService> mockPhotoService;
        private UserService service;
        private Mock<IMediator> mockMediator;

        private UserDto existingUserDTO;
        private User existingUser;

        private Guid userId = Guid.NewGuid();
        private NotificationChange notificationTypeId = NotificationChange.OwnEvent;
        private UserNotificationType userNotificationType;
        private string name = "existingName";
        private string existingEmail = "existingEmail@gmail.com";

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockPhotoService = new Mock<IPhotoService>();
            mockMediator = new Mock<IMediator>();

            service = new UserService(
                Context,
                MockMapper.Object,
                mockMediator.Object,
                mockPhotoService.Object);

            existingUser = new User
            {
                Id = userId,
                Name = name,
                Email = existingEmail,
            };

            existingUserDTO = new UserDto
            {
                Id = userId,
                Name = name,
                Email = existingEmail,
            };

            userNotificationType = new UserNotificationType
            {
                UserId = userId,
                User = existingUser,
                NotificationTypeId = NotificationChange.OwnEvent,
            };

            Context.Users.Add(existingUser);
            Context.UserNotificationTypes.Add(userNotificationType);
            Context.SaveChanges();
        }

        [Test]
        public async Task GetUnblockedUsersCount_ReturnsValid()
        {
            // Arrange
            var expected = await Context.Users.CountAsync(user => !user.Account.IsBlocked);

            // Act
            var actual = await service.CountUnblockedUsersAsync();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUserByNotificationType_NotificationChange_IEnumerable_userIds_userExisting()
        {
            var idsUsers = new[] { userId };
            var res = service.GetUsersByNotificationTypes(notificationTypeId, idsUsers);
            var resUsers = res.Where(x => idsUsers.Contains(x.Id)).Select(x => x);
            Assert.That(resUsers.Where(x => x.Name.Contains(name) && x.Email.Contains(existingEmail)), Is.Not.Null);
        }

        [Test]
        public void GetUserByNotificationType_NotificationChange_IEnumerable_userIds_userNotExisting()
        {
            var idsUsers = new[] { Guid.NewGuid() };
            var res = service.GetUsersByNotificationTypes(notificationTypeId, idsUsers);
            Assert.That(res, Is.Empty);
        }

        [Test]
        public void EditFavoriteNotificationTypes_CorrectNotificationChange_CorrectUser_NotThrowAsync()
        {
            var notificationTypes = new[] { new NotificationType { Id = NotificationChange.Profile } };
            Assert.DoesNotThrowAsync(async () => await service.EditFavoriteNotificationTypes(existingUserDTO, notificationTypes));
        }

        [Test]
        public void EditFavoriteNotificationTypes_CorrectNotificationChange_InCorrectUser_ThrowAsync()
        {
            var notificationTypes = new[] { new NotificationType { Id = NotificationChange.Profile } };
            var notExistingUser = existingUserDTO;
            notExistingUser.Id = Guid.NewGuid();
            Assert.ThrowsAsync<InvalidOperationException>(async () => await service.EditFavoriteNotificationTypes(notExistingUser, notificationTypes));
        }

        [Test]
        public void EditFavoriteNotificationTypes_InCorrectNotificationChange_CorrectUser_ThrowAsync()
        {
            var notificationTypes = new[] { new NotificationType { Id = (NotificationChange)(-888) } };
            Assert.DoesNotThrowAsync(async () => await service.EditFavoriteNotificationTypes(existingUserDTO, notificationTypes));
        }

        [Test]
        public void EditFavoriteNotificationTypes_InCorrectNotificationChange_InCorrectUser_ThrowAsync()
        {
            var notificationTypes = new[] { new NotificationType { Id = (NotificationChange)(-888) } };
            var notExistingUser = existingUserDTO;
            notExistingUser.Id = Guid.NewGuid();
            Assert.ThrowsAsync<InvalidOperationException>(async () => await service.EditFavoriteNotificationTypes(notExistingUser, notificationTypes));
        }

        [Test]
        public void Create_RepeatEmail_ReturnFalse()
        {
            UserDto newUser = new UserDto()
            {
                Id = Guid.NewGuid(),
                Email = existingUserDTO.Email,
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(newUser));
        }

        [Test]

        public void Create_ValidDto_ReturnTrue()
        {
            var correctAccountId = Guid.NewGuid();
            UserDto newUserDTO = new UserDto()
            {
                Email = "correctemail@example.com",
                AccountId = correctAccountId,
            };
            User newUser = new User()
            {
                Email = "correctemail@example.com",
                Account = new Account
                {
                    Id = correctAccountId,
                },
            };

            MockMapper.Setup(m => m
                .Map<User>(newUserDTO))
                    .Returns(newUser);

            Assert.DoesNotThrowAsync(async () => await service.Create(newUserDTO));
        }

        [Test]
        public void Create_InsertFailed_ReturnFalse()
        {
            MockMapper.Setup(m => m
                .Map<User>(existingUserDTO))
                    .Returns(existingUser);

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(existingUserDTO));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Update_EmailIsNull_ReturnFalse(string email)
        {
            UserDto newUser = new UserDto() { Email = email };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Update(newUser));
        }

        [Test]
        public void Update_UserInDbNotFound_Throws()
        {
            existingUserDTO.Id = Guid.NewGuid();
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Update(existingUserDTO));
        }

        [Test]
        public void Update_UserDtoIsvalid_DoesNotThrow()
        {
            MockMapper.Setup(m => m
                .Map<UserDto, User>(existingUserDTO))
                    .Returns(existingUser);

            Assert.DoesNotThrowAsync(async () => await service.Update(existingUserDTO));
        }

        [Test]
        public void ChangeAvatar_UserInDbNotFound_Throws()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.ChangeAvatar(It.IsAny<Guid>(), It.IsAny<FormFile>()));
        }

        [Test]
        public void ChangeAvatar_UserInDbFound_Success()
        {
            string testFilePath = @"./Images/valid-image.jpg";
            byte[] bytes = File.ReadAllBytes(testFilePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(testFilePath);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            var file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };

            Assert.DoesNotThrowAsync(async () => await service.ChangeAvatar(userId, file));
            mockPhotoService.Verify(x => x.AddUserPhoto(file, userId));
        }

        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
