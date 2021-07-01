using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class UsersServiceTests : TestInitializer
    {
        private static Mock<IPhotoService> mockPhotoService;
        private Guid secondUserId = Guid.NewGuid();
        private Mock<ISecurityContext> mockSecurityContext;
        private UserService service;

        private UserDto existingUserDTO;
        private User existingUser;
        private User secondUser;

        private Guid userId = Guid.NewGuid();
        private NotificationChange notificationTypeId = NotificationChange.OwnEvent;

        private Category[] categories =
        {
            new Category { Id = Guid.NewGuid(), Name = "Sea" },
            new Category { Id = Guid.NewGuid(), Name = "Mount" },
            new Category { Id = Guid.NewGuid(), Name = "Summer" },
            new Category { Id = Guid.NewGuid(), Name = "Golf" },
        };

        private string name = "existingName";
        private string secondName = "secondName";
        private string existingEmail = "existingEmail@gmail.com";
        private string secondEmail = "secondEmail@gmail.com";

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            mockPhotoService = new Mock<IPhotoService>();
            mockSecurityContext = new Mock<ISecurityContext>();
            MockMapper.Setup(opts => opts.Map<IEnumerable<CategoryDto>>(It.IsAny<IEnumerable<UserCategory>>()))
                .Returns((IEnumerable<UserCategory> u) => u.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name }));

            service = new UserService(
                Context,
                MockMapper.Object,
                mockPhotoService.Object,
                mockSecurityContext.Object);

            existingUser = new User
            {
                Id = userId,
                Name = name,
                Email = existingEmail,
                NotificationTypes = new List<UserNotificationType>
                {
                    new UserNotificationType
                    {
                        UserId = userId,
                        NotificationTypeId = NotificationChange.OwnEvent,
                    },
                    new UserNotificationType
                    {
                        UserId = userId,
                        NotificationTypeId = NotificationChange.Profile,
                    },
                },
                Categories = new List<UserCategory>
                {
                    new UserCategory
                    {
                        UserId = userId,
                        CategoryId = categories[0].Id,
                    },
                },
            };

            secondUser = new User
            {
                Id = secondUserId,
                Name = secondName,
                Email = secondEmail,
                NotificationTypes = new List<UserNotificationType>
                {
                    new UserNotificationType
                    {
                        UserId = secondUserId,
                        NotificationTypeId = NotificationChange.Profile,
                    },
                },
                Categories = new List<UserCategory>
                {
                    new UserCategory
                    {
                        UserId = secondUserId,
                        CategoryId = categories[1].Id,
                    },
                },
            };

            existingUserDTO = new UserDto
            {
                Id = userId,
                Name = name,
                Email = existingEmail,
                NotificationTypes = new List<UserNotificationType>
                {
                    new UserNotificationType
                    {
                        UserId = userId,
                        NotificationTypeId = NotificationChange.OwnEvent,
                    },
                    new UserNotificationType
                    {
                        UserId = userId,
                        NotificationTypeId = NotificationChange.Profile,
                    },
                },
                Categories = new List<UserCategory>
                {
                    new UserCategory
                    {
                        UserId = userId,
                        CategoryId = categories[0].Id,
                    },
                },
            };

            Context.Users.Add(existingUser);
            Context.Users.Add(secondUser);
            Context.Categories.AddRange(categories);
            Context.SaveChanges();
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
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            var notificationTypes = new[] { new NotificationType { Id = NotificationChange.Profile } };
            Assert.DoesNotThrowAsync(async () => await service.EditFavoriteNotificationTypes(notificationTypes));
        }

        [Test]
        public void EditFavoriteNotificationTypes_CorrectNotificationChange_InCorrectUser_ThrowAsync()
        {
            var notificationTypes = new[] { new NotificationType { Id = NotificationChange.Profile } };
            var notExistingUserId = Guid.NewGuid();
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(notExistingUserId);
            Assert.ThrowsAsync<InvalidOperationException>(async () => await service.EditFavoriteNotificationTypes(notificationTypes));
        }

        [Test]
        public void EditFavoriteNotificationTypes_InCorrectNotificationChange_CorrectUser_ThrowAsync()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            var notificationTypes = new[] { new NotificationType { Id = (NotificationChange)(-888) } };
            Assert.DoesNotThrowAsync(async () => await service.EditFavoriteNotificationTypes(notificationTypes));
        }

        [Test]
        public void GetUserNotificationTypes_True()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new NotificationTypeMapperProfile());
            });
            MockMapper.Setup(s => s.ConfigurationProvider).Returns(mockMapperConfig);
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            var res = service.GetUserNotificationTypes();
            Assert.AreEqual(2, res.Count());
        }

        [Test]
        public void GetUserCategories_True()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryMapperProfile());
            });
            MockMapper.Setup(s => s.ConfigurationProvider).Returns(mockMapperConfig);
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            var res = service.GetUserCategories();
            Assert.AreEqual(1, res.Count());
        }

        [Test]
        public void EditFavoriteNotificationTypes_InCorrectNotificationChange_InCorrectUser_ThrowAsync()
        {
            var notificationTypes = new[] { new NotificationType { Id = (NotificationChange)(-888) } };
            var notExistingUserId = Guid.NewGuid();
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(notExistingUserId);
            Assert.ThrowsAsync<InvalidOperationException>(async () => await service.EditFavoriteNotificationTypes(notificationTypes));
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
        public void EditUserName_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            Assert.DoesNotThrowAsync(async () => await service.EditUserName(It.IsAny<string>()));
        }

        [Test]
        public void EditBirthday_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            Assert.DoesNotThrowAsync(async () => await service.EditBirthday(It.IsAny<DateTime>()));
        }

        [Test]
        public void EditGender_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            Assert.DoesNotThrowAsync(async () => await service.EditGender(It.IsAny<Gender>()));
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
