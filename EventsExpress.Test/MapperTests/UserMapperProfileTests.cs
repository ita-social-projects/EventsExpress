using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class UserMapperProfileTests : MapperTestInitializer<UserMapperProfile>
    {
        private UserDto firstUserDto;
        private Guid idUser = Guid.NewGuid();
        private Guid idRole = Guid.NewGuid();

        private List<User> GetListUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Id = idUser,
                    Name = "User",
                    Email = "user@gmail.com",
                    Birthday = DateTime.Now,
                },
            };
        }

        private User GetUser()
        {
            List<User> users = GetListUsers();
            return new User
            {
                Id = idUser,
                Name = "First user",
            };
        }

        private UserDto GetUserDto()
        {
            List<User> users = GetListUsers();
            return new UserDto
            {
                Id = idUser,
                Name = "first user",
                Email = "admin@gmail.com",
                Phone = "+38066666666",
                Birthday = new DateTime(2001, 01, 01),
                Gender = Gender.Male,
                Role = new Role { Name = "admin", Users = new List<User> { } },
                RoleId = Guid.NewGuid(),
                Rating = 10.0,
                IsBlocked = false,
                Categories = new List<UserCategory>
                {
                    new UserCategory
                    {
                        UserId = idUser,
                        Category = new Category { Name = "some" },
                    },
                },
                NotificationTypes = new List<UserNotificationType>
                {
                    new UserNotificationType
                    {
                        UserId = idUser,
                        NotificationType = new NotificationType { Name = "not" },
                    },
                },
            };
        }

        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();

            IServiceCollection services = new ServiceCollection();
            var mock = new Mock<IPhotoService>();
            services.AddTransient<IPhotoService>(sp => mock.Object);

            services.AddAutoMapper(typeof(UserMapperProfile));

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Mapper = serviceProvider.GetService<IMapper>();
            mock.Setup(x => x.GetPhotoFromAzureBlob(It.IsAny<string>())).Returns(Task.FromResult("test"));
        }

        [Test]
        public void UserMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void UserMapperProfile_UserDtoToUserPreviewViewModel()
        {
            firstUserDto = GetUserDto();
            var resEven = Mapper.Map<UserDto, UserPreviewViewModel>(firstUserDto);

            Assert.That(resEven.PhotoUrl, Is.EqualTo("test"));
            Assert.That(resEven.Id, Is.EqualTo(firstUserDto.Id));
            Assert.That(resEven.Username, Is.EqualTo(firstUserDto.Name ?? firstUserDto.Email.Substring(0, firstUserDto.Email.IndexOf("@", StringComparison.Ordinal))));
            Assert.That(resEven.Email, Is.EqualTo(firstUserDto.Email));
            Assert.That(resEven.Birthday, Is.EqualTo(firstUserDto.Birthday));
            Assert.That(resEven.Rating, Is.EqualTo(firstUserDto.Rating));
        }

        [Test]
        public void UserMapperProfile_UserDtoToUserInfoViewModel()
        {
            firstUserDto = GetUserDto();
            var resEven = Mapper.Map<UserDto, UserInfoViewModel>(firstUserDto);

            Assert.That(resEven.PhotoUrl, Is.EqualTo("test"));
            Assert.That(resEven.Id, Is.EqualTo(firstUserDto.Id));
            Assert.That(resEven.Name, Is.EqualTo(firstUserDto.Name));
            Assert.That(resEven.Email, Is.EqualTo(firstUserDto.Email));
            Assert.That(resEven.Birthday, Is.EqualTo(firstUserDto.Birthday));
            Assert.That(resEven.Rating, Is.EqualTo(firstUserDto.Rating));
            Assert.That(resEven.Gender, Is.EqualTo((int)firstUserDto.Gender));
            Assert.That(resEven.Role, Is.EqualTo(firstUserDto.Role.Name));
            Assert.That(resEven.Categories, Has.All.Matches<CategoryViewModel>(x =>
                firstUserDto.Categories
                .All(c =>
                    x.Id == c.CategoryId &&
                    x.Name == c.Category.Name)));
            Assert.That(resEven.NotificationTypes, Has.All.Matches<NotificationTypeViewModel>(x =>
                firstUserDto.NotificationTypes
                .All(c =>
                    x.Id == c.NotificationTypeId &&
                    x.Name == c.NotificationType.Name)));
        }

        [Test]
        public void UserMapperProfile_UserDtoToUserManageViewModel()
        {
            firstUserDto = GetUserDto();
            var resEven = Mapper.Map<UserDto, UserManageViewModel>(firstUserDto);

            Assert.That(resEven.PhotoUrl, Is.EqualTo("test"));
            Assert.That(resEven.Id, Is.EqualTo(firstUserDto.Id));
            Assert.That(resEven.Username, Is.EqualTo(firstUserDto.Name ?? firstUserDto.Email.Substring(0, firstUserDto.Email.IndexOf("@", StringComparison.Ordinal))));
            Assert.That(resEven.Email, Is.EqualTo(firstUserDto.Email));
            Assert.That(resEven.Birthday, Is.EqualTo(firstUserDto.Birthday));
            Assert.That(resEven.Rating, Is.EqualTo(firstUserDto.Rating));
            Assert.That((int)resEven.Gender, Is.EqualTo((int)firstUserDto.Gender));
            Assert.That(resEven.IsBlocked, Is.EqualTo(firstUserDto.IsBlocked));
        }
    }
}
