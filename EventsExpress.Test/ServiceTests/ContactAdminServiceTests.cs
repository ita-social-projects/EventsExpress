using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class ContactAdminServiceTests : TestInitializer
    {
        private ContactAdminService _service;
        private ContactAdmin _contactAdmin;
        private ContactAdmin _contactAdminTest;
        private Guid messageId = Guid.NewGuid();
        private ContactAdminStatus issueStatus = ContactAdminStatus.Resolve;
        private List<ContactAdmin> messages;
        private ContactAdminDto contactAdminDTO;
        private ContactAdminFilterViewModel contactAdminFilterViewModel;
        private string resolutionDetails = "any details";

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            messages = new List<ContactAdmin>
            {
                new ContactAdmin
                {
                    Id = Guid.NewGuid(),
                    Title = "SLdndsndj",
                    Status = ContactAdminStatus.InProgress,
                },
                new ContactAdmin
                {
                    Id = Guid.NewGuid(),
                    Title = "other title",
                    Status = ContactAdminStatus.Open,
                },
            };

            contactAdminFilterViewModel = new ContactAdminFilterViewModel()
            {
                Page = 1,
                PageSize = 5,
                Status = new List<ContactAdminStatus> { ContactAdminStatus.Open, ContactAdminStatus.InProgress, ContactAdminStatus.Resolve },
            };

            _contactAdmin = new ContactAdmin
            {
                Id = messageId,
                Title = "any title",
                Status = ContactAdminStatus.Open,
                Email = "testEmail",
                EmailBody = "anyDescription",
                ResolutionDetails = "anyResolution",
            };

            _contactAdminTest = new ContactAdmin
            {
                Id = Guid.NewGuid(),
                Title = "test title",
                Status = ContactAdminStatus.Resolve,
                Email = "aneEmail",
                EmailBody = "testDescription",
                ResolutionDetails = "testResolution",
            };

            contactAdminDTO = new ContactAdminDto()
            {
                MessageId = Guid.NewGuid(),
                Status = ContactAdminStatus.Open,
                Title = "any title",
                Email = "testEmail",
                MessageText = "anyDescription",
                ResolutionDetails = "anyResolution",
            };

            MockMapper
                .Setup(m => m.Map<ContactAdminDto>(It.IsAny<ContactAdmin>()))
                .Returns(new ContactAdminDto
                {
                    MessageId = _contactAdmin.Id,
                });

            _service = new ContactAdminService(
                Context,
                MockMapper.Object);

            Context.ContactAdmin.Add(_contactAdmin);
            Context.ContactAdmin.AddRange(messages);
            Context.SaveChanges();
        }

        [Test]
        [Category("Update issue status")]
        public void UpdateIssueStatus_InvalidId_ThrowsException()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await _service.UpdateIssueStatus(Guid.Empty, resolutionDetails, issueStatus));
        }

        [Test]
        [Category("Update issue status")]
        public void UpdateIssueStatus_ReturnTrue()
        {
            var message = Context.ContactAdmin.Find(_contactAdmin.Id);
            message.Status = issueStatus;
            Assert.DoesNotThrowAsync(async () => await _service.UpdateIssueStatus(message.Id, resolutionDetails, message.Status));
        }

        [Test]
        [Category("Message by id")]
        public void GetMessageById_IsValid()
        {
            var result = _service.MessageById(messageId);
            Assert.AreEqual(_contactAdmin.Id, result.MessageId);
        }

        [Test]
        [Category("Message by id")]
        public void GetMessageById_ReturnMessage()
        {
            var message = _service.MessageById(messageId);
            Assert.That(message, Is.Not.Null);
        }

        [Test]
        [Category("Get All")]
        public void GetAll_GetMessageByStatus_Success()
        {
            ContactAdminFilterViewModel contactAdminFilterViewModel = new ContactAdminFilterViewModel()
            {
                Status = new List<ContactAdminStatus> { ContactAdminStatus.Open, ContactAdminStatus.InProgress, ContactAdminStatus.Resolve },
            };
            var count = messages.Count;
            _service.GetAll(contactAdminFilterViewModel, out count);
            Assert.AreEqual(3, count);
        }

        [Test]
        [Category("Get All")]
        public void GetAll_GetAllMessages_Success()
        {
            int count = 1;
            MockMapper.Setup(u => u.Map<IEnumerable<ContactAdmin>, IEnumerable<ContactAdminDto>>(It.IsAny<IEnumerable<ContactAdmin>>()))
                .Returns((IEnumerable<ContactAdmin> e) => e?.Select(item => new ContactAdminDto { MessageId = item.Id }));
            var result = _service.GetAll(contactAdminFilterViewModel, out count);
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        [Category("SendMessageToAdmin")]
        public void SendMessage_ContactAdminDtoIsvalid_DoesNotThrow()
        {
            MockMapper.Setup(m => m.Map<ContactAdminDto, ContactAdmin>(contactAdminDTO))
                    .Returns(_contactAdminTest);

            Assert.DoesNotThrowAsync(async () => await _service.SendMessageToAdmin(contactAdminDTO));
        }

        [Test]
        [Category("SendMessageToAdmin")]
        public void SendMessageToAdmin_Success()
        {
            MockMapper.Setup(u => u.Map<ContactAdminDto, ContactAdmin>(It.IsAny<ContactAdminDto>()))
                .Returns((ContactAdminDto e) => e == null ?
                null :
                new ContactAdmin
                {
                    Id = e.MessageId,
                    EmailBody = e.MessageText,
                    Email = e.Email,
                    Title = e.Title,
                    Status = e.Status,
                    Subject = e.Subject,
                });

            Assert.DoesNotThrowAsync(async () => await _service.SendMessageToAdmin(contactAdminDTO));
        }
    }
}
