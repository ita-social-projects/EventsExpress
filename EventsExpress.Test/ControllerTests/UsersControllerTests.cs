using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using EventsExpress.ViewModels.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Role = EventsExpress.Db.Enums.Role;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class UsersControllerTests
    {
        private Mock<IUserService> _userService;
        private Mock<IMapper> _mapper;
        private UsersController _usersController;
        private UserDto _userDto;
        private Guid _idUser = Guid.NewGuid();
        private string _userEmal = "user@gmail.com";
        private EditUserNotificationTypesViewModel editUserNotificationTypesView;
        private NotificationTypeDto firstNotificationTypeDto;
        private AttitudeViewModel _attitudeViewModel;
        private AttitudeDto _attitudeDto;
        private EditUserCategoriesViewModel _editUserCategoriesViewModel;
        private CategoryDto _firstCategoryDto;
        private EditUserGenderViewModel _editUserGenderViewModel;
        private short _gender = 8;
        private EditUserBirthViewModel _editUserBirthViewModel;
        private DateTime _birthdeay = new DateTime(2000, 9, 6);
        private EditUserNameViewModel _editUserNameViewModel;
        private EditFirstNameViewModel _editFirstNameViewModel;
        private EditLastNameViewModel _editLastNameViewModel;
        private LocationViewModel _editLocationViewModel;
        private string _userName = "some name of user";
        private string _firstName = "some firstName of user";
        private string _lastName = "some lastName of user";
        private UsersFilterViewModel _usersFilterViewModel;
        private int _pageSize = 5;
        private int _page = 8;

        private LocationDto _location = new LocationDto
        {
            Point = null, OnlineMeeting = "string", Type = LocationType.Online,
        };

        [SetUp]
        public void Initialize()
        {
            _userService = new Mock<IUserService>();
            _mapper = new Mock<IMapper>();

            _usersController = new UsersController(_userService.Object, _mapper.Object);
            _userDto = new UserDto
            {
                Id = _idUser,
                Email = _userEmal,
            };

            _mapper.Setup(u => u.Map<IEnumerable<NotificationTypeDto>, IEnumerable<NotificationType>>(It.IsAny<IEnumerable<NotificationTypeDto>>()))
            .Returns((IEnumerable<NotificationTypeDto> e) => e.Select(item => new NotificationType { Id = item.Id, Name = item.Name }));

            _mapper.Setup(u => u.Map<IEnumerable<AttitudeViewModel>, IEnumerable<AttitudeDto>>(It.IsAny<IEnumerable<AttitudeViewModel>>()))
            .Returns((IEnumerable<AttitudeViewModel> e) => e.Select(item => new AttitudeDto { UserFromId = item.UserFromId, UserToId = item.UserToId, Attitude = item.Attitude }));

            _mapper.Setup(u => u.Map<IEnumerable<CategoryDto>, IEnumerable<Category>>(It.IsAny<IEnumerable<CategoryDto>>()))
            .Returns((IEnumerable<CategoryDto> e) => e.Select(item => new Category { Id = item.Id, Name = item.Name }));

            _mapper.Setup(u => u.Map<IEnumerable<UserDto>, IEnumerable<UserManageViewModel>>(It.IsAny<IEnumerable<UserDto>>()))
            .Returns((IEnumerable<UserDto> e) => e.Select(item => new UserManageViewModel { Id = item.Id }));

            firstNotificationTypeDto = new NotificationTypeDto
            {
                Id = NotificationChange.OwnEvent,
                Name = NotificationChange.OwnEvent.ToString(),
            };
            editUserNotificationTypesView = new EditUserNotificationTypesViewModel
            {
                NotificationTypes = new NotificationTypeDto[] { firstNotificationTypeDto },
            };
            _attitudeViewModel = new AttitudeViewModel
            {
                Attitude = 2,
                UserFromId = Guid.NewGuid(),
                UserToId = Guid.NewGuid(),
            };
            _attitudeDto = new AttitudeDto
            {
                Attitude = 2,
                UserFromId = Guid.NewGuid(),
                UserToId = Guid.NewGuid(),
            };
            _firstCategoryDto = new CategoryDto { Id = Guid.NewGuid(), Name = "Category name" };
            _editUserCategoriesViewModel = new EditUserCategoriesViewModel { Categories = new CategoryDto[] { _firstCategoryDto } };
            _editUserGenderViewModel = new EditUserGenderViewModel { Gender = _gender };
            _editUserBirthViewModel = new EditUserBirthViewModel { Birthday = _birthdeay };
            _editUserNameViewModel = new EditUserNameViewModel { Name = _userName };
            _editFirstNameViewModel = new EditFirstNameViewModel { FirstName = _firstName };
            _editLastNameViewModel = new EditLastNameViewModel { LastName = _lastName };
            _editLocationViewModel = new LocationViewModel { Latitude = 0.0, Longitude = 0.0, Type = LocationType.Map, };
            _usersFilterViewModel = new UsersFilterViewModel { Page = _page, PageSize = _pageSize };
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            _usersController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(15)]
        public async Task GetUsersCount_ReturnsValid(int count)
        {
            // Arrange
            _userService.Setup(service => service.CountUsersAsync(It.IsAny<AccountStatus>()))
                .ReturnsAsync(count);

            // Act
            var actionResult = (OkObjectResult)(await _usersController.Count(It.IsAny<AccountStatus>())).Result;
            var actual = (int)actionResult.Value;

            // Assert
            Assert.AreEqual(count, actual);
        }

        [Test]
        public async Task GetUsersCount_CalledMethodOfService()
        {
            // Act
            await _usersController.Count(It.IsAny<AccountStatus>());

            // Assert
            _userService.Verify(service => service.CountUsersAsync(It.IsAny<AccountStatus>()), Times.Once);
        }

        [Test]
        [Category("EditUserNotificationType")]
        public async Task EditUserNotificationType_ExistsUserDto_OkObjectResultAsync()
        {
            _userService.Setup(us => us.EditFavoriteNotificationTypes(It.IsAny<NotificationType[]>())).Returns(Task.FromResult(_userDto.Id));

            var res = await _usersController.EditUserNotificationType(editUserNotificationTypesView);
            Assert.IsInstanceOf<IActionResult>(res);
            OkObjectResult okResult = res as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var assertId = okResult.Value;
            Assert.That(assertId, Is.EqualTo(_userDto.Id));
            _userService.Verify(us => us.EditFavoriteNotificationTypes(It.IsAny<NotificationType[]>()), Times.Exactly(1));
        }

        [Test]
        [Category("SetAttitude")]
        public async Task SetAttitude_CorrectAttitude_DoesNotThrowAsync()
        {
            _userService.Setup(user => user.SetAttitude(_attitudeDto)).Returns((Task t) => t);
            var res = await _usersController.SetAttitude(_attitudeViewModel);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
        }

        [Test]
        [Category("GetUserProfileById")]
        public void GetUserProfileById_CorrectAttitude_DoesNotThrow()
        {
            Guid userId = Guid.NewGuid();
            Guid fromId = Guid.NewGuid();
            ProfileDto profileDto = new ProfileDto
            {
                Id = Guid.NewGuid(),
            };

            _userService.Setup(us => us.GetProfileById(userId)).Returns(profileDto);
            _mapper.Setup(u => u.Map<ProfileDto, ProfileViewModel>(It.IsAny<ProfileDto>()))
            .Returns((ProfileDto e) => new ProfileViewModel { Id = e.Id });
            var res = _usersController.GetUserProfileById(userId);
            Assert.DoesNotThrow(() => Task.FromResult(res));
            Assert.IsInstanceOf<IActionResult>(res);
            OkObjectResult okResult = res as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [Category("EditUserCategory")]
        public async Task EditUserCategory_UserDto_OkObjectResultAsync()
        {
            _userService.Setup(us => us.EditFavoriteCategories(It.IsAny<Category[]>())).Returns(Task.FromResult(_userDto.Id));

            var res = await _usersController.EditUserCategory(_editUserCategoriesViewModel);
            Assert.IsInstanceOf<IActionResult>(res);
            _userService.Verify(us => us.EditFavoriteCategories(It.IsAny<Category[]>()), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserGender")]
        public async Task EditUserGender_CorrectUserDto_OkResultAsync()
        {
            _userService.Setup(u => u.EditGender(It.IsAny<Gender>()));

            var res = await _usersController.EditGender(_editUserGenderViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            OkResult okObjectResult = res as OkResult;
            _userService.Verify(u => u.EditGender(It.IsAny<Gender>()), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserBirthday")]
        public async Task EditUserBirthday_UserDto_OkResultAsync()
        {
            _userService.Setup(user => user.EditBirthday(It.IsAny<DateTime>()));

            var res = await _usersController.EditBirthday(_editUserBirthViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _userService.Verify(us => us.EditBirthday(It.IsAny<DateTime>()), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserName")]
        public async Task EditUserName_UserDto_OkObjectResultAsync()
        {
            _userService.Setup(user => user.EditUserName(It.IsAny<string>()));

            var res = await _usersController.EditUsername(_editUserNameViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _userService.Verify(us => us.EditUserName(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserFirstName")]
        public async Task EditFirstName_UserDto_OkObjectResultAsync()
        {
            _userService.Setup(user => user.EditFirstName(It.IsAny<string>()));

            var res = await _usersController.EditFirstName(_editFirstNameViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _userService.Verify(us => us.EditFirstName(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserLastName")]
        public async Task EditLastName_UserDto_OkObjectResultAsync()
        {
            _userService.Setup(user => user.EditLastName(It.IsAny<string>()));

            var res = await _usersController.EditLastName(_editLastNameViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _userService.Verify(us => us.EditLastName(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        [Category("EditLocation")]
        public async Task EditLocation_UserDto_OkObjectResultAsync()
        {
            _userService.Setup(user => user.EditLocation(It.IsAny<LocationDto>()));

            var res = await _usersController.EditLocation(_editLocationViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<IActionResult>(res);
            _userService.Verify(us => us.EditLocation(It.IsAny<LocationDto>()), Times.Exactly(1));
        }

        [Test]
        [Category("Get")]
        public void Get_NotNull_Exception()
        {
            int count = 0;
            _userService.Setup(a => a.Get(_usersFilterViewModel, out count)).Throws<EventsExpressException>();

            Assert.Throws<EventsExpressException>(() => _usersController.Get(_usersFilterViewModel));
            _userService.Verify(us => us.Get(_usersFilterViewModel, out count), Times.Exactly(1));
        }

        [Test]
        [Category("Get")]
        public void Get_NotNull_BadRequestResult()
        {
            int count = 10;
            _userService.Setup(a => a.Get(_usersFilterViewModel, out count)).Throws<ArgumentOutOfRangeException>();

            var res = _usersController.Get(_usersFilterViewModel);

            Assert.IsInstanceOf<BadRequestResult>(res);
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            _userService.Verify(us => us.Get(_usersFilterViewModel, out count), Times.Exactly(1));
        }

        [Test]
        [Category("Get")]
        public void Get_NotNull_OkObjectResult()
        {
            int count = 0;
            _userService.Setup(user => user.Get(It.IsAny<UsersFilterViewModel>(), out count)).Returns(new UserDto[] { _userDto });

            var res = _usersController.Get(_usersFilterViewModel);

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        [Category("GetNotificationTypes")]
        public void GetNotifications_OkResult()
        {
            _userService.Setup(user => user.GetUserNotificationTypes()).Returns(new NotificationTypeDto[] { firstNotificationTypeDto });
            var res = _usersController.GetNotificationTypes();

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        [Category("GetCategories")]
        public void GetCategories_OkResult()
        {
            _userService.Setup(user => user.GetUserCategories()).Returns(new CategoryDto[] { _firstCategoryDto });
            var res = _usersController.GetCategories();

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        [Category("SearchUsers")]
        public void SearchUsers_NotNull_Exception()
        {
            int count = 0;
            _userService.Setup(a => a.Get(_usersFilterViewModel, out count)).Throws<EventsExpressException>();

            Assert.Throws<EventsExpressException>(() => _usersController.SearchUsers(_usersFilterViewModel));
            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count), Times.Exactly(1));
        }

        [Test]
        [Category("SearchUsers")]
        public void SearchUsers_NotNull_BadRequestResult()
        {
            int count = 0;
            _userService.Setup(a => a.Get(_usersFilterViewModel, out count)).Throws<ArgumentOutOfRangeException>();

            var res = _usersController.SearchUsers(_usersFilterViewModel);
            Assert.That(res, Is.TypeOf<BadRequestResult>());
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));

            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count), Times.Exactly(1));
        }

        [Test]
        [Category("SearchUsers")]
        public void SearchUsers_NotNull_OkObjectResult1()
        {
            int count = 0;
            _userService.Setup(user => user.Get(It.IsAny<UsersFilterViewModel>(), out count)).Returns(new UserDto[] { _userDto });

            var res = _usersController.SearchUsers(_usersFilterViewModel);

            Assert.IsInstanceOf<OkObjectResult>(res);
            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count), Times.Exactly(1));
        }

        [Test]
        [Category("SearchUsersShortInformation")]
        public void SearchUsersShortInformation_NotNull_Exception()
        {
            int count = 0;
            _userService.Setup(a => a.Get(_usersFilterViewModel, out count)).Throws<EventsExpressException>();

            Assert.Throws<EventsExpressException>(() => _usersController.SearchUsersShortInformation(_usersFilterViewModel));
            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count), Times.Exactly(1));
        }

        [Test]
        [Category("SearchUsersShortInformation")]
        public void SearchUsersShortInformation_NotNull_BadRequestResult()
        {
            int count = 0;
            _userService.Setup(a => a.Get(_usersFilterViewModel, out count)).Throws<ArgumentOutOfRangeException>();

            var res = _usersController.SearchUsersShortInformation(_usersFilterViewModel);
            Assert.That(res, Is.TypeOf<BadRequestResult>());
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));

            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count), Times.Exactly(1));
        }

        [Test]
        [Category("SearchUsersShortInformation")]
        public void SearchUsersShortInformation_NotNull_OkObjectResult()
        {
            int count = 0;
            _userService.Setup(user => user.Get(It.IsAny<UsersFilterViewModel>(), out count)).Returns(new UserDto[] { _userDto });

            var res = _usersController.SearchUsersShortInformation(_usersFilterViewModel);

            Assert.IsInstanceOf<OkObjectResult>(res);
            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count), Times.Exactly(1));
        }

        [Test]
        public void GetUsersShortInformation_IdsNotNull_OkObjectResult()
        {
            var ids = new[] { _userDto.Id };
            _userService.Setup(service => service.GetUsersInformationByIds(ids))
                        .Returns(new[] { _userDto });

            var result = _usersController.GetUsersShortInformation(ids);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetUsersShortInformation_IdsNull_OkObjectResult()
        {
            _userService.Setup(service => service.GetUsersInformationByIds(null))
                        .Returns(Array.Empty<UserDto>());

            var result = _usersController.GetUsersShortInformation(null);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        [Category("GetUserInfo")]
        public void GetUserInfo_Success()
        {
            _userService.Setup(a => a.GetCurrentUserInfo()).Returns(_userDto);
            _mapper.Setup(m => m.Map<UserDto, UserInfoViewModel>(_userDto))
                .Returns(new UserInfoViewModel { Email = _userDto.Email });

            var res = _usersController.GetUserInfo();

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _userService.Verify(us => us.GetCurrentUserInfo(), Times.Once);
            Assert.That((res as OkObjectResult).Value != null);
        }

        [Test]
        [Category("GetUserInfo")]
        public void GetUserInfo_NullResult()
        {
            _userService.Setup(a => a.GetCurrentUserInfo()).Returns((UserDto)null);

            var res = _usersController.GetUserInfo();

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkObjectResult>(res);
            _userService.Verify(us => us.GetCurrentUserInfo(), Times.Once);
            Assert.That((res as OkObjectResult).Value == null);
        }
    }
}
