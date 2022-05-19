using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Test.ServiceTests.TestClasses.Comparers;
using EventsExpress.Test.ServiceTests.TestClasses.Location;
using EventsExpress.Test.ServiceTests.TestClasses.Photo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Moq;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using Location = EventsExpress.Db.Entities.Location;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class UsersServiceTests : TestInitializer
    {
        private static Mock<IUserPhotoService> mockUserPhotoService;
        private Mock<ISecurityContext> mockSecurityContext;
        private UserService service;
        private Mock<IMediator> mockMediator;
        private Mock<ILocationManager> mockLocationService;

        private UserDto existingUserDTO;
        private UserDto secondUserDTO;
        private User existingUser;
        private User secondUser;

        private Guid userId = Guid.NewGuid();
        private Guid secondUserId = Guid.NewGuid();
        private NotificationChange notificationTypeId = NotificationChange.OwnEvent;

        private string name = "existingName";
        private string secondName = "secondName";
        private string existingEmail = "existingEmail@gmail.com";
        private string secondEmail = "secondEmail@gmail.com";

        private UserDtoComparer userDtoComparer;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            Clear(Context.UserCategory);
            mockUserPhotoService = new Mock<IUserPhotoService>();
            mockMediator = new Mock<IMediator>();
            mockSecurityContext = new Mock<ISecurityContext>();
            MockMapper.Setup(opts => opts.Map<IEnumerable<CategoryDto>>(It.IsAny<IEnumerable<UserCategory>>()))
                .Returns((IEnumerable<UserCategory> u) => u.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name }));
            MockMapper.Setup(opts => opts.Map<IEnumerable<NotificationTypeDto>>(It.IsAny<IEnumerable<UserNotificationType>>()))
                .Returns((IEnumerable<UserNotificationType> u) => u.Select(x => new NotificationTypeDto { Id = x.NotificationType.Id, Name = x.NotificationType.Name }));
            MockMapper.Setup(opts => opts.Map<User>(It.IsAny<UserDto>()))
                .Returns((UserDto u) => new User()
                {
                });
            MockMapper.Setup(opts => opts.Map<UserDto>(It.IsAny<User>()))
                .Returns((User u) => new UserDto()
                {
                    Id = u.Id,
                });
            MockMapper.Setup(opts => opts.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>()))
                      .Returns((IEnumerable<User> users) =>
                          users.Select(user => new UserDto { Id = user.Id }));
            mockLocationService = new Mock<ILocationManager>();

            service = new UserService(
                Context,
                MockMapper.Object,
                mockMediator.Object,
                mockUserPhotoService.Object,
                mockLocationService.Object,
                mockSecurityContext.Object);

            existingUser = new User
            {
                Id = userId,
                Name = name,
                Email = existingEmail,
                LocationId = Guid.NewGuid(),
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
                    Category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sea",
                    },
                },
            },
                Account = new Account
                {
                    UserId = userId,
                    IsBlocked = true,
                },
                Location = new Location
                {
                    Point = Point.Empty,
                    OnlineMeeting = null,
                    Type = LocationType.Map,
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
                    Category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Mount",
                    },
                },
            },
                Account = new Account
                {
                    UserId = secondUserId,
                    IsBlocked = false,
                },
                LocationId = null,
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
                    Category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sea",
                    },
                },
            },
                Location = new LocationDto
                {
                    Point = Point.Empty,
                    OnlineMeeting = null,
                    Type = LocationType.Map,
                    Id = Guid.NewGuid(),
                },
            };

            secondUserDTO = new UserDto
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
                    Category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "Mount",
                    },
                },
            },
                Account = new Account
                {
                    UserId = secondUserId,
                    IsBlocked = false,
                },
                Location = null,
            };

            userDtoComparer = new UserDtoComparer();

            Context.Users.Add(existingUser);
            Context.Users.Add(secondUser);

            Context.SaveChanges();
        }

        [Test]
        public void GetUsersInformationByIds_WhenIdsArePassed_ReturnsListWithFoundUsers()
        {
            var ids = new[] { existingUser.Id, secondUser.Id };
            var expected = new[] { existingUserDTO, secondUserDTO };

            var actual = service.GetUsersInformationByIds(ids);

            Assert.That(actual, Is.EquivalentTo(expected).Using(userDtoComparer));
        }

        [Test]
        public void GetUsersInformationByIds_WhenNoIdsPassed_ReturnsEmptyList()
        {
            const int expectedLength = 0;
            var emptyIds = Array.Empty<Guid>();

            var actual = service.GetUsersInformationByIds(emptyIds);
            var actualLength = actual.Count();

            Assert.AreEqual(expectedLength, actualLength);
        }

        [TestCase(AccountStatus.All)]
        [TestCase(AccountStatus.Activated)]
        [TestCase(AccountStatus.Blocked)]
        public async Task CountUsersAsync_GivenAccountStatus_ReturnsCorrectCount(AccountStatus status)
        {
            var expected = status switch
            {
                AccountStatus.Activated => await Context.Users.CountAsync(u => !u.Account.IsBlocked),
                AccountStatus.Blocked => await Context.Users.CountAsync(u => u.Account.IsBlocked),
                _ => await Context.Users.CountAsync(),
            };

            var actual = await service.CountUsersAsync(status);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_InvalidAccount_Throws()
        {
            existingUserDTO.Email = "test";
            existingUserDTO.AccountId = Guid.Empty;

            var ex = Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(existingUserDTO));
            Assert.That(ex.Message, Contains.Substring("Account not found"));
        }

        [Test]
        public void GetCurrentUserInfo_ExistingUser_ReturnsCorrectId()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUser.Id);
            var expected = existingUserDTO;

            var actual = service.GetCurrentUserInfo();

            Assert.AreEqual(expected.Id, actual.Id);
        }

        [Test]
        public void GetUsersByNotificationTypes_ExistingUser_Success()
        {
            var idsUsers = new[] { userId };

            var res = service.GetUsersByNotificationTypes(notificationTypeId, idsUsers);
            var resUsers = res.Where(x => idsUsers.Contains(x.Id)).Select(x => x);

            Assert.That(resUsers.Where(x => x.Name.Contains(name) && x.Email.Contains(existingEmail)), Is.Not.Null);
        }

        [Test]
        public void GetUsersByNotificationTypes_InvalidUser_ReturnsEmptyResult()
        {
            var idsUsers = new[] { Guid.NewGuid() };

            var res = service.GetUsersByNotificationTypes(notificationTypeId, idsUsers);

            Assert.That(res, Is.Empty);
        }

        [Test]
        public void EditFavoriteNotificationTypes_CorrectChangeAndUser_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            var notificationTypes = new[] { new NotificationType { Id = NotificationChange.Profile } };

            Assert.DoesNotThrowAsync(async () =>
                await service.EditFavoriteNotificationTypes(notificationTypes));
        }

        [Test]
        public void EditFavoriteNotificationTypes_CorrectChangeWithInvalidUser_Throws()
        {
            var notificationTypes = new[] { new NotificationType { Id = NotificationChange.Profile } };
            var notExistingUserId = Guid.NewGuid();
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(notExistingUserId);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.EditFavoriteNotificationTypes(notificationTypes));
        }

        [Test]
        public void EditFavoriteNotificationTypes_IncorrectChangeWithExistingUser_Throws()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);
            var notificationTypes = new[] { new NotificationType { Id = (NotificationChange)(-888) } };

            Assert.DoesNotThrowAsync(async () =>
                await service.EditFavoriteNotificationTypes(notificationTypes));
        }

        [Test]
        public void EditFavoriteNotificationTypes_IncorrectChangeAndUser_Throws()
        {
            var notificationTypes = new[] { new NotificationType { Id = (NotificationChange)(-888) } };
            var notExistingUserId = Guid.NewGuid();
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(notExistingUserId);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.EditFavoriteNotificationTypes(notificationTypes));
        }

        [Test]
        public void GetUserNotificationTypes_ExistingUser_Success()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            var res = service.GetUserNotificationTypes();

            Assert.AreEqual(2, res.Count());
        }

        [Test]
        public void GetUserCategories_ExistingUser_Success()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            var res = service.GetUserCategories();

            Assert.AreEqual(1, res.Count());
        }

        [Test]
        public void Create_RepeatEmail_Throws()
        {
            var newUser = new UserDto
            {
                Id = Guid.NewGuid(),
                Email = existingUserDTO.Email,
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(newUser));
        }

        [Test]
        public void Create_ValidDto_DoesNotThrow()
        {
            var correctAccountId = Guid.NewGuid();
            var newUserDto = new UserDto
            {
                Email = "correctemail@example.com",
                AccountId = correctAccountId,
            };
            var newUser = new User
            {
                Email = "correctemail@example.com",
                Account = new Account
                {
                    Id = correctAccountId,
                },
            };

            MockMapper.Setup(m => m.Map<User>(newUserDto)).Returns(newUser);

            Assert.DoesNotThrowAsync(async () => await service.Create(newUserDto));
        }

        [Test]
        public void Create_InsertFailed_Throws()
        {
            MockMapper.Setup(m => m.Map<User>(existingUserDTO)).Returns(existingUser);

            Assert.ThrowsAsync<EventsExpressException>(async () =>
                await service.Create(existingUserDTO));
        }

        [Test]
        public void EditUserName_ExistingUser_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            Assert.DoesNotThrowAsync(async () =>
                await service.EditUserName(It.IsAny<string>()));
        }

        [Test]
        public void EditLastName_ExistingUser_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            Assert.DoesNotThrowAsync(async () =>
                await service.EditLastName(It.IsAny<string>()));
        }

        [Test]
        public void EditFirstName_ExistingUser_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            Assert.DoesNotThrowAsync(async () =>
                await service.EditFirstName(It.IsAny<string>()));
        }

        [Test]
        public void EditBirthday_ExistingUser_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            Assert.DoesNotThrowAsync(async () =>
                    await service.EditBirthday(It.IsAny<DateTime>()));
        }

        [Test]
        [TestCaseSource(typeof(CreatingNotExistingUserLocation))]
        public void EditLocation_LocationTypeOnline_Throws(LocationDto locationDto)
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            Assert.ThrowsAsync<EventsExpressException>(async () =>
                await service.EditLocation(locationDto));
        }

        [Test]
        public void EditLocation_UserLocationIdIsNull_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(secondUser.Id);

            Assert.DoesNotThrowAsync(async () => await service.EditLocation(existingUserDTO.Location));
        }

        [Test]
        public void EditGender_ExistingUser_DoesNotThrow()
        {
            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            Assert.DoesNotThrowAsync(async () => await service.EditGender(It.IsAny<Gender>()));
        }

        [Test]
        public void ChangeAvatar_UserInDbFound_Success()
        {
            string testFilePath = @"./Images/valid-event-image.jpg";
            using var stream = new MemoryStream();
            var file = PhotoHelpers.GetPhoto(testFilePath, stream);

            mockSecurityContext.Setup(s => s.GetCurrentUserId()).Returns(existingUserDTO.Id);

            Assert.DoesNotThrowAsync(async () => await service.ChangeAvatar(file));
            mockUserPhotoService.Verify(x => x.AddUserPhoto(file, existingUserDTO.Id));
        }

        [Test]
        public void ChangeAvatar_InvalidFile_Throws()
        {
            mockUserPhotoService.Setup(ps => ps.AddUserPhoto(It.IsAny<IFormFile>(), It.IsAny<Guid>())).Throws<ArgumentException>();
            var formFile = new FormFile(new MemoryStream(), 0, 0, string.Empty, "test");

            var ex = Assert.ThrowsAsync<EventsExpressException>(async () => await service.ChangeAvatar(formFile));
            Assert.That(ex.Message, Contains.Substring("Bad image file"));
        }

        private static void Clear<T>(DbSet<T> dbSet)
            where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
