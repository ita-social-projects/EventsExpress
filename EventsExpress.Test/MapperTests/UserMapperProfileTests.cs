using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private Guid idUser2 = Guid.NewGuid();
        private Guid idAccount = Guid.NewGuid();
        private Guid idRole = Guid.NewGuid();
        private User firstUser;
        private AppDbContext context;
        private Mock<IHttpContextAccessor> mockAccessor;
        private Mock<IUserService> mockUser;
        private Mock<ISecurityContext> mockSecurityContextService;

        private User GetUser()
        {
            return new User
            {
                Id = idUser,
                Name = "first user",
                Email = "admin@gmail.com",
                Phone = "+38066666666",
                Birthday = new DateTime(2001, 01, 01),
                Gender = Gender.Male,
                Account = new Account
                {
                    AccountRoles = new[]
                    {
                        new AccountRole
                        {
                            Role = new Db.Entities.Role
                            {
                                Name = "Admin",
                                Id = Db.Enums.Role.Admin,
                            },
                        },
                    },
                    IsBlocked = false,
                },
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
                Relationships = new List<Relationship>
                {
                    new Relationship
                    {
                        Attitude = Attitude.Dislike,
                        UserFromId = idUser,
                        UserToId = idUser2,
                    },
                },
            };
        }

        private UserDto GetUserDto()
        {
            return new UserDto
            {
                Id = idUser,
                Name = "first user",
                Email = "admin@gmail.com",
                Phone = "+38066666666",
                Birthday = new DateTime(2001, 01, 01),
                Gender = Gender.Male,
                Account = new Account
                {
                    AccountRoles = new[]
                    {
                        new AccountRole
                        {
                            Role = new Db.Entities.Role
                            {
                                Name = "Admin",
                                Id = Db.Enums.Role.Admin,
                            },
                        },
                    },
                    IsBlocked = false,
                },
                Rating = 10.0,
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

        private ProfileDto GetProfileDto() => Mapper.Map<ProfileDto>(GetUserDto());

        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();

            IServiceCollection services = new ServiceCollection();
            var mockAuth = new Mock<IAuthService>();
            mockUser = new Mock<IUserService>();
            mockAccessor = new Mock<IHttpContextAccessor>();
            mockSecurityContextService = new Mock<ISecurityContext>();

            mockAccessor.Setup(sp => sp.HttpContext.User);
            mockUser.Setup(sp => sp.GetRating(It.IsAny<Guid>())).Returns(5);

            services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase(databaseName: "db"));
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "db"));
            services.AddTransient(sp => mockAuth.Object);
            services.AddTransient(sp => mockAccessor.Object);
            services.AddTransient(sp => mockUser.Object);
            services.AddTransient(sp => mockSecurityContextService.Object);

            services.AddAutoMapper(typeof(EventMapperProfile));
            services.AddAutoMapper(typeof(UserMapperProfile));

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Mapper = serviceProvider.GetService<IMapper>();
            context = serviceProvider.GetService<AppDbContext>();

            mockSecurityContextService.Setup(x => x.GetCurrentUserId()).Returns(idUser);

            context.Users.Add(GetUser());
            context.SaveChanges();
        }

        [Test]
        public void UserMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void UserMapperProfile_UserToUserDto()
        {
            firstUser = context.Users.FirstOrDefault(x => x.Id == GetUser().Id);
            var resEven = Mapper.Map<User, UserDto>(firstUser);

            Assert.That(resEven.Attitude, Is.EqualTo(1));
            Assert.That(resEven.Rating, Is.EqualTo(5));
            Assert.That(resEven.CanChangePassword, Is.EqualTo(false));
        }

        [Test]
        public void UserMapperProfile_UserDtoToUserPreviewViewModel()
        {
            firstUserDto = GetUserDto();
            var resEven = Mapper.Map<UserDto, UserPreviewViewModel>(firstUserDto);

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

            Assert.That(resEven.Id, Is.EqualTo(firstUserDto.Id));
            Assert.That(resEven.Name, Is.EqualTo(firstUserDto.Name));
            Assert.That(resEven.Email, Is.EqualTo(firstUserDto.Email));
            Assert.That(resEven.Birthday, Is.EqualTo(firstUserDto.Birthday));
            Assert.That(resEven.Rating, Is.EqualTo(firstUserDto.Rating));
            Assert.That(resEven.Gender, Is.EqualTo((int)firstUserDto.Gender));
            Assert.That(resEven.Roles.Count(), Is.EqualTo(firstUserDto.Account.AccountRoles.Count()));
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

            Assert.That(resEven.Id, Is.EqualTo(firstUserDto.Id));
            Assert.That(resEven.Username, Is.EqualTo(firstUserDto.Name ?? firstUserDto.Email.Substring(0, firstUserDto.Email.IndexOf("@", StringComparison.Ordinal))));
            Assert.That(resEven.Email, Is.EqualTo(firstUserDto.Email));
            Assert.That(resEven.Birthday, Is.EqualTo(firstUserDto.Birthday));
            Assert.That(resEven.Rating, Is.EqualTo(firstUserDto.Rating));
            Assert.That((int)resEven.Gender, Is.EqualTo((int)firstUserDto.Gender));
            Assert.That(resEven.IsBlocked, Is.EqualTo(firstUserDto.Account.IsBlocked));
        }

        [Test]
        public void UserMapperProfile_ProfileDtoToUProfileViewModel()
        {
            var firstProfileDto = GetProfileDto();
            var resEven = Mapper.Map<ProfileDto, ProfileViewModel>(firstProfileDto);

            Assert.That(resEven.Attitude, Is.EqualTo(1));
            Assert.That(resEven.Rating, Is.EqualTo(5));
        }
    }
}
