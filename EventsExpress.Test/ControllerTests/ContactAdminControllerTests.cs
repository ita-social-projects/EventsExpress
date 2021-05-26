using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    internal class ContactAdminControllerTests
    {
        private Mock<IContactAdminService> _contactAdminService;
        private Mock<IMapper> _mapper;
        private ContactAdminController _contactAdminController;
        private Guid id = Guid.NewGuid();
        private UpdateIssueStatusViewModel issueStatus;
        private ContactAdminDto contactAdminDTO;
        private ContactAdminFilterViewModel filter;
        private ContactAdminViewModel model;
        private int count = 1;
        private ContactAdminFilterViewModel contactAdminFilterViewModel;
        private int _pageSize = 5;
        private int _page = 8;

        [SetUp]
        public void Initialize()
        {
            _contactAdminService = new Mock<IContactAdminService>();
            _mapper = new Mock<IMapper>();
            _contactAdminController = new ContactAdminController(_mapper.Object, _contactAdminService.Object);
            contactAdminDTO = new ContactAdminDto()
            {
                MessageId = Guid.NewGuid(),
                Status = ContactAdminStatus.InProgress,
                Title = "any title",
            };

            filter = new ContactAdminFilterViewModel()
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                Status = new List<ContactAdminStatus>()
                {
                    ContactAdminStatus.InProgress,
                    ContactAdminStatus.Open,
                    ContactAdminStatus.Resolve,
                },
            };

            model = new ContactAdminViewModel()
            {
                Description = "some description",
                Subject = ContactAdminReason.BadEvent,
                Title = "any title",
                Email = "any email",
                DateCreated = DateTime.Now,
            };

            issueStatus = new UpdateIssueStatusViewModel { MessageId = id, ResolutionDetails = "anyResolution", Status = ContactAdminStatus.InProgress };

            contactAdminFilterViewModel = new ContactAdminFilterViewModel
            {
                Page = _page,
                PageSize = _pageSize,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                Status = new List<ContactAdminStatus>()
                {
                    ContactAdminStatus.InProgress,
                    ContactAdminStatus.Open,
                    ContactAdminStatus.Resolve,
                },
            };
        }

        [Test]
        [Category("All")]
        public void GetAll_NotNull_OkObjectResult()
        {
            _contactAdminService.Setup(message => message.GetAll(It.IsAny<ContactAdminFilterViewModel>(), out count)).Returns(new ContactAdminDto[] { contactAdminDTO });

            var res = _contactAdminController.All(filter);

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        [Category("All")]
        public void GetAll_NotNull_BadRequestResult()
        {
            _contactAdminService.Setup(message => message.GetAll(It.IsAny<ContactAdminFilterViewModel>(), out count)).Returns(new ContactAdminDto[] { contactAdminDTO });
            var res = _contactAdminController.All(contactAdminFilterViewModel);

            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.IsInstanceOf<BadRequestResult>(res);
        }

        [Test]
        [Category("UpdateStatus")]
        public async Task UpdateStatus_OkResult()
        {
            _contactAdminService.Setup(item => item.UpdateIssueStatus(issueStatus.MessageId, issueStatus.ResolutionDetails, issueStatus.Status)).Returns(Task.CompletedTask);

            var expected = await _contactAdminController.UpdateStatus(issueStatus);
            Assert.DoesNotThrowAsync(() => Task.FromResult(expected));
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        [Category("UpdateStatus")]
        public void UpdateStatus_ThrowsException()
        {
            _contactAdminService.Setup(item => item.UpdateIssueStatus(issueStatus.MessageId, issueStatus.ResolutionDetails, issueStatus.Status)).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _contactAdminController.UpdateStatus(issueStatus));
        }

        [Test]
        [Category("GetMessageById")]
        public void GetMessageById_NotNull_OkObjectResult()
        {
            _contactAdminService.Setup(item => item.MessageById(It.IsAny<Guid>())).Returns(contactAdminDTO);
            var testGuid = contactAdminDTO.MessageId;
            var okResult = _contactAdminController.GetMessageById(testGuid);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.IsNotNull(okResult);
        }

        [Test]
        [Category("ContactAdmins")]
        public async Task ContactAdmins_CorrectDTO_OkResult()
        {
            _contactAdminService.Setup(x => x.SendMessageToAdmin(It.IsAny<ContactAdminDto>()))
                        .Returns((ContactAdminDto e) => Task.FromResult(contactAdminDTO.MessageId));

            var res = await _contactAdminController.ContactAdmins(model);
            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<IActionResult>(res);
        }
    }
}
