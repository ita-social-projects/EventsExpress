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
    internal class ContactUSControllerTests
    {
        private Mock<IContactAdminService> _contactAdminService;
        private Mock<IMapper> _mapper;
        private ContactUsController _contactUsController;
        private Guid id = Guid.NewGuid();
        private ContactUsViewModel issueStatus;
        private ContactAdminDto contactAdminDTO;
        private ContactAdminFilterViewModel filter;
        private ContactUsViewModel model;
        private int count = 1;
        private ContactAdminFilterViewModel contactUsFilterViewModel;
        private int _pageSize = 5;
        private int _page = 8;

        [SetUp]
        public void Initialize()
        {
            _contactAdminService = new Mock<IContactAdminService>();
            _mapper = new Mock<IMapper>();
            _contactUsController = new ContactUsController(_mapper.Object, _contactAdminService.Object);
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

            model = new ContactUsViewModel()
            {
                Description = "some description",
                Subject = ContactAdminReason.BadEvent,
                Title = "any title",
                Email = "any email",
                DateCreated = DateTime.Now,
            };

            issueStatus = new ContactUsViewModel { MessageId = id, Status = ContactAdminStatus.InProgress };

            contactUsFilterViewModel = new ContactAdminFilterViewModel
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
            _contactAdminService.Setup(message => message.GetAll(It.IsAny<ContactAdminFilterViewModel>(), It.IsAny<Guid>(), out count)).Returns(new ContactAdminDto[] { contactAdminDTO });

            var res = _contactUsController.All(filter, id);

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        [Category("All")]
        public void GetAll_NotNull_BadRequestResult()
        {
            _contactAdminService.Setup(message => message.GetAll(It.IsAny<ContactAdminFilterViewModel>(), It.IsAny<Guid>(), out count)).Returns(new ContactAdminDto[] { contactAdminDTO });
            var res = _contactUsController.All(contactUsFilterViewModel, id);

            BadRequestResult badResult = res as BadRequestResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
            Assert.IsInstanceOf<BadRequestResult>(res);
        }

        [Test]
        [Category("UpdateStatus")]
        public async Task UpdateStatus_OkResult()
        {
            _contactAdminService.Setup(item => item.UpdateIssueStatus(issueStatus.MessageId, issueStatus.Status)).Returns(Task.CompletedTask);

            var expected = await _contactUsController.UpdateStatus(issueStatus);
            Assert.DoesNotThrowAsync(() => Task.FromResult(expected));
            Assert.IsInstanceOf<OkObjectResult>(expected);
        }

        [Test]
        [Category("UpdateStatus")]
        public void UpdateStatus_ThrowsException()
        {
            _contactAdminService.Setup(item => item.UpdateIssueStatus(issueStatus.MessageId, issueStatus.Status)).Throws<EventsExpressException>();

            Assert.ThrowsAsync<EventsExpressException>(() => _contactUsController.UpdateStatus(issueStatus));
        }

        [Test]
        [Category("GetMessageById")]
        public void GetMessageById_NotNull_OkObjectResult()
        {
            _contactAdminService.Setup(item => item.MessageById(It.IsAny<Guid>())).Returns(contactAdminDTO);
            var testGuid = contactAdminDTO.MessageId;
            var okResult = _contactUsController.GetMessageById(testGuid);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.IsNotNull(okResult);
        }

        [Test]
        [Category("ContactAdmins")]
        public async Task ContactAdmins_OkResult()
        {
            var res = await _contactUsController.ContactAdmins(model, id);

            Assert.DoesNotThrowAsync(() => Task.FromResult(res));
            Assert.IsInstanceOf<IActionResult>(res);
            _contactAdminService.Verify(message => message.SendMessageToAdmin(It.IsAny<ContactAdminDto>()), Times.Exactly(1));
        }
    }
}
