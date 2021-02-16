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
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class UsersControllerTests
    {
        private Mock<IUserService> _userService;
        private Mock<IAuthService> _authService;
        private Mock<IMapper> _mapper;
        private Mock<IEmailService> _emailService;
        private UsersController _usersController;
        private UserDto _userDto;
        private Guid _idUser = Guid.NewGuid();
        private string _userEmal = "user@gmail.com";
        private EditUserNotificationTypesViewModel editUserNotificationTypesView;
        private EditUserNotificationTypesViewModel editUserNotificationTypesViewInValid;
        private NotificationTypeDto firstNotificationTypeDto;
        private NotificationTypeDto firstNotificationTypeDtoInvalid;
        private AttitudeViewModel _attitudeViewModel;
        private AttitudeDto _attitudeDto;
        private EditUserCategoriesViewModel _editUserCategoriesViewModel;
        private CategoryDto _firstCategoryDto;
        private EditUserGenderViewModel _editUserGenderViewModel;
        private short _gender = 8;
        private EditUserBirthViewModel _editUserBirthViewModel;
        private DateTime _birthdeay = new DateTime(2000, 9, 6);
        private EditUserNameViewModel _editUserNameViewModel;
        private string _userName = "some name of user";
        private Guid _idRole = Guid.NewGuid();
        private UsersFilterViewModel _usersFilterViewModel;
        private int _pageSize = 5;
        private int _page = 8;

        [SetUp]
        public void Initialize()
        {
            _userService = new Mock<IUserService>();
            _authService = new Mock<IAuthService>();
            _mapper = new Mock<IMapper>();
            _emailService = new Mock<IEmailService>();
            _usersController = new UsersController(_userService.Object, _authService.Object, _mapper.Object, _emailService.Object);
            _userDto = new UserDto
            {
                Id = _idUser,
                Email = _userEmal,
                Photo = new Photo { Id = Guid.NewGuid(), Img = new byte[8], Thumb = new byte[8] },
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
            firstNotificationTypeDtoInvalid = new NotificationTypeDto
            {
                Id = NotificationChange.OwnEvent,
                Name = string.Empty,
            };
            editUserNotificationTypesView = new EditUserNotificationTypesViewModel
            {
                NotificationTypes = new NotificationTypeDto[] { firstNotificationTypeDto },
            };
            editUserNotificationTypesViewInValid = new EditUserNotificationTypesViewModel
            {
                NotificationTypes = new NotificationTypeDto[] { firstNotificationTypeDtoInvalid },
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
            _usersFilterViewModel = new UsersFilterViewModel { Page = _page, PageSize = _pageSize };

            var user = new ClaimsPrincipal(new ClaimsIdentity());
            _usersController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }

        [Test]
        [Category("EditUserNotificationType")]
        public async Task EditUserNotificationType_ExistsUserDto_OkObjectResultAsync()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            _userService.Setup(us => us.EditFavoriteNotificationTypes(_userDto, It.IsAny<NotificationType[]>())).Returns(Task.FromResult(_userDto.Id));

            var res = await _usersController.EditUserNotificationType(editUserNotificationTypesView);
            Assert.IsInstanceOf<IActionResult>(res);
            OkObjectResult okResult = res as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var assertId = okResult.Value;
            Assert.That(assertId, Is.EqualTo(_userDto.Id));
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.EditFavoriteNotificationTypes(_userDto, It.IsAny<NotificationType[]>()), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserNotificationType")]
        public void EditUserNotificationType_NULL_Throws()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns((UserDto)null);
            Assert.ThrowsAsync<EventsExpressException>(async () => await _usersController.EditUserNotificationType(editUserNotificationTypesView));
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
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            Guid userId = Guid.NewGuid();
            Guid fromId = Guid.NewGuid();
            ProfileDto profileDto = new ProfileDto
            {
                Id = Guid.NewGuid(),
            };

            _userService.Setup(us => us.GetProfileById(userId, _idUser)).Returns(profileDto);
            _mapper.Setup(u => u.Map<ProfileDto, ProfileViewModel>(It.IsAny<ProfileDto>()))
            .Returns((ProfileDto e) => new ProfileViewModel { Id = e.Id });
            var res = _usersController.GetUserProfileById(userId);
            Assert.DoesNotThrow(() => Task.FromResult(res));
            Assert.IsInstanceOf<IActionResult>(res);
            OkObjectResult okResult = res as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
        }

        [Test]
        [Category("ContactAdmins")]
        public async Task ContactAdmins_CorrectAdmins_ContactCorrectCountPartsAsync()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            string roleName = "Admin";
            Role role = new Role { Id = Guid.NewGuid(), Name = roleName };
            UserDto firstAdmin = new UserDto { Id = Guid.NewGuid(), Role = role };
            UserDto secondAdmin = new UserDto { Id = Guid.NewGuid(), Role = role };
            var admins = new UserDto[] { firstAdmin, secondAdmin };
            _userService.Setup(user => user.GetUsersByRole(roleName)).Returns(admins);
            ContactUsViewModel model = new ContactUsViewModel { Description = "some description", Type = "some type" };

            var res = await _usersController.ContactAdmins(model);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<IActionResult>(res);
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(user => user.GetUsersByRole(roleName), Times.Exactly(1));
            _emailService.Verify(email => email.SendEmailAsync(It.IsAny<EmailDto>()), Times.Exactly(admins.Length));
        }

        [Test]
        [Category("ChangeAvatar")]
        public async Task ChangeAvatar_NULL_BadRequestResult()
        {
            _userService.Setup(user => user.ChangeAvatar(It.IsAny<Guid>(), It.IsAny<IFormFile>()));
            _userService.Setup(user => user.GetById(It.IsAny<Guid>())).Returns((UserDto user) => user);
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns((UserDto)null);

            var res = await _usersController.ChangeAvatar();

            Assert.IsInstanceOf<BadRequestResult>(res);
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(user => user.ChangeAvatar(It.IsAny<Guid>(), It.IsAny<IFormFile>()), Times.Exactly(0));
            _userService.Verify(user => user.GetById(It.IsAny<Guid>()), Times.Exactly(0));
        }

        [Test]
        [Category("ChangeAvatar")]
        public async Task ChangeAvatar_CorrectUser_OkObjectResult()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            _userService.Setup(user => user.ChangeAvatar(_userDto.Id, It.IsAny<IFormFile>()));
            _userService.Setup(user => user.GetById(_userDto.Id)).Returns(_userDto);
            _usersController.ControllerContext.HttpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            _usersController.ControllerContext.HttpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { file });

            var res = await _usersController.ChangeAvatar();

            Assert.IsInstanceOf<OkObjectResult>(res);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            OkObjectResult okResult = res as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(user => user.ChangeAvatar(_userDto.Id, It.IsAny<IFormFile>()), Times.Exactly(1));
            _userService.Verify(user => user.GetById(_userDto.Id), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserCategory")]
        public async Task EditUserCategory_UserDto_OkObjectResultAsync()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            _userService.Setup(us => us.EditFavoriteCategories(_userDto, It.IsAny<Category[]>())).Returns(Task.FromResult(_userDto.Id));

            var res = await _usersController.EditUserCategory(_editUserCategoriesViewModel);
            Assert.IsInstanceOf<IActionResult>(res);
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.EditFavoriteCategories(_userDto, It.IsAny<Category[]>()), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserCategory")]
        public async Task EditUserCategory_NULL_BadRequestResult()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns((UserDto)null);
            _userService.Setup(us => us.EditFavoriteCategories(_userDto, It.IsAny<Category[]>())).Returns(Task.FromResult(_userDto.Id));

            var res = await _usersController.EditUserCategory(_editUserCategoriesViewModel);

            Assert.IsInstanceOf<BadRequestResult>(res);
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.EditFavoriteCategories(_userDto, It.IsAny<Category[]>()), Times.Exactly(0));
        }

        [Test]
        [Category("EditUserGender")]
        public async Task EditUserGender_NULL_BadRequestResult()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns((UserDto)null);
            _userService.Setup(user => user.Update(It.IsAny<UserDto>()));

            var res = await _usersController.EditGender(_editUserGenderViewModel);

            Assert.IsInstanceOf<BadRequestResult>(res);
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Update(It.IsAny<UserDto>()), Times.Exactly(0));
        }

        [Test]
        [Category("EditUserGender")]
        public async Task EditUserGender_CorrectUserDto_OkResultAsync()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            _userService.Setup(user => user.Update(_userDto));

            var res = await _usersController.EditGender(_editUserGenderViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            OkResult okObjectResult = res as OkResult;
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Update(_userDto), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserBirthday")]
        public async Task EditUserBirthday_NULL_BadRequestResult()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns((UserDto)null);
            _userService.Setup(user => user.Update(It.IsAny<UserDto>()));

            var res = await _usersController.EditBirthday(_editUserBirthViewModel);

            Assert.IsInstanceOf<BadRequestResult>(res);
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Update(It.IsAny<UserDto>()), Times.Exactly(0));
        }

        [Test]
        [Category("EditUserBirthday")]
        public async Task EditUserBirthday_UserDto_OkResultAsync()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            _userService.Setup(user => user.Update(_userDto));

            var res = await _usersController.EditBirthday(_editUserBirthViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Update(_userDto), Times.Exactly(1));
        }

        [Test]
        [Category("EditUserName")]
        public async Task EditUserName_NULL_BadRequestResult()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns((UserDto)null);
            _userService.Setup(user => user.Update(It.IsAny<UserDto>()));

            var res = await _usersController.EditUsername(_editUserNameViewModel);

            Assert.IsInstanceOf<BadRequestResult>(res);
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Update(It.IsAny<UserDto>()), Times.Exactly(0));
        }

        [Test]
        [Category("EditUserName")]
        public async Task EditUserName_UserDto_OkObjectResultAsync()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            _userService.Setup(user => user.Update(_userDto));

            var res = await _usersController.EditUsername(_editUserNameViewModel);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _authService.Verify(aut => aut.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Update(_userDto), Times.Exactly(1));
        }

        [Test]
        [Category("Block")]
        public void Block_CorrectIdUser_ThrowException()
        {
            Guid userId = _userDto.Id;
            _userService.Setup(user => user.Block(It.IsAny<Guid>())).Throws<EventsExpressException>();
            Assert.ThrowsAsync<EventsExpressException>(() => _usersController.Block(userId));
        }

        [Test]
        [Category("Block")]
        public async Task Block_CorrectIdUser_OkResultAsync()
        {
            Guid userId = _userDto.Id;
            _userService.Setup(user => user.Block(userId));

            var res = await _usersController.Block(userId);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _userService.Verify(us => us.Block(It.IsAny<Guid>()), Times.Exactly(1));
        }

        [Test]
        [Category("UnBlock")]
        public void UnBlock_CorrectIdUser_ThrowException()
        {
            _userService.Setup(user => user.Unblock(It.IsAny<Guid>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _usersController.Unblock(It.IsAny<Guid>()));

            _userService.Verify(us => us.Unblock(It.IsAny<Guid>()), Times.Exactly(1));
        }

        [Test]
        [Category("UnBlock")]
        public async Task UnBlock_CorrectIdUser_OkResultAsync()
        {
            Guid userId = _userDto.Id;
            _userService.Setup(user => user.Unblock(userId));

            var res = await _usersController.Unblock(userId);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _userService.Verify(us => us.Unblock(It.IsAny<Guid>()), Times.Exactly(1));
        }

        [Test]
        [Category("ChangeRole")]
        public void ChangeRole_IdUser_ThrowException()
        {
            _userService.Setup(user => user.ChangeRole(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _usersController.ChangeRole(It.IsAny<Guid>(), It.IsAny<Guid>()));
            _userService.Verify(us => us.ChangeRole(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Exactly(1));
        }

        [Test]
        [Category("ChangeRole")]
        public async Task ChangeRole_IdUser_OkResultAsync()
        {
            Guid idUser = _idUser;
            Guid idRole = _idRole;
            _userService.Setup(user => user.ChangeRole(idUser, idRole));

            var res = await _usersController.ChangeRole(idUser, idRole);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<OkResult>(res);
            _userService.Verify(us => us.ChangeRole(idUser, idRole), Times.Exactly(1));
        }

        [Test]
        [Category("Get")]
        public void Get_NotNull_Exception()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Throws<EventsExpressException>();

            Assert.Throws<EventsExpressException>(() => _usersController.Get(_usersFilterViewModel));
            _authService.Verify(us => us.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
        }

        [Test]
        [Category("Get")]
        public void Get_NotNull_BadRequestResult()
        {
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Throws<ArgumentOutOfRangeException>();

            var res = _usersController.Get(_usersFilterViewModel);

            Assert.IsInstanceOf<BadRequestResult>(res);
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            _authService.Verify(us => us.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
        }

        [Test]
        [Category("Get")]
        public void Get_NotNull_OkObjectResult()
        {
            int count = 0;
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            _userService.Setup(user => user.Get(It.IsAny<UsersFilterViewModel>(), out count, It.IsAny<Guid>())).Returns(new UserDto[] { _userDto });

            var res = _usersController.Get(_usersFilterViewModel);

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        [Category("SearchUsers")]
        public void SearchUsers_NotNull_Exception()
        {
            int count = 0;
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Throws<EventsExpressException>();

            Assert.Throws<EventsExpressException>(() => _usersController.SearchUsers(_usersFilterViewModel));
            _authService.Verify(us => us.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count, It.IsAny<Guid>()), Times.Exactly(0));
        }

        [Test]
        [Category("SearchUsers")]
        public void SearchUsers_NotNull_BadRequestResult()
        {
            int count = 0;
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Throws<ArgumentOutOfRangeException>();

            var res = _usersController.SearchUsers(_usersFilterViewModel);
            Assert.IsInstanceOf<BadRequestResult>(res);
            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));

            _authService.Verify(us => us.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count, It.IsAny<Guid>()), Times.Exactly(0));
        }

        [Test]
        [Category("SearchUsers")]
        public void SearchUsers_NotNull_OkObjectResult1()
        {
            int count = 0;
            _authService.Setup(a => a.GetCurrentUser(It.IsAny<ClaimsPrincipal>())).Returns(_userDto);
            _userService.Setup(user => user.Get(It.IsAny<UsersFilterViewModel>(), out count, It.IsAny<Guid>())).Returns(new UserDto[] { _userDto });

            var res = _usersController.SearchUsers(_usersFilterViewModel);

            Assert.IsInstanceOf<OkObjectResult>(res);
            _authService.Verify(us => us.GetCurrentUser(It.IsAny<ClaimsPrincipal>()), Times.Exactly(1));
            _userService.Verify(us => us.Get(It.IsAny<UsersFilterViewModel>(), out count, It.IsAny<Guid>()), Times.Exactly(1));
        }
    }
}
