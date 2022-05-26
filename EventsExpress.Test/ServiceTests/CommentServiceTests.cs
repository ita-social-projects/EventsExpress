using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class CommentServiceTests : TestInitializer
    {
        private CommentService service;
        private Comments existingComment;
        private User user;
        private Event evnt;
        private Guid eventId = Guid.NewGuid();
        private Guid commentId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CommentService(Context, MockMapper.Object);

            user = new User
            {
                Id = userId,
                FirstName = "NameIsExist",
                Email = "user@gmail.com",
            };

            evnt = new Event
            {
                Id = eventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "...",
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory { EventStatus = EventStatus.Active },
                },
            };

            existingComment = new Comments
            {
                Text = "Test comment",
                Id = commentId,
                Date = new DateTime(2019, 08, 08),
                EventId = eventId,
                UserId = userId,
            };

            Context.Events.Add(evnt);
            Context.Users.Add(user);
            Context.Comments.Add(existingComment);
            Context.SaveChanges();
        }

        [Test]
        public void Delete_Existing_returnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.Delete(commentId));
        }

        [Test]
        public void Delete_NotExisting_returnFalse()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Delete(Guid.NewGuid()));
        }

        [Test]
        public void Create_RigthData_returnTrue()
        {
            CommentDto comment = new CommentDto()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                User = user,
                Text = "Text",
            };

            Assert.DoesNotThrowAsync(async () => await service.Create(comment));
        }

        [Test]
        public void Create_WrongUserId_returnFalse()
        {
            CommentDto comment = new CommentDto()
            {
                Id = commentId,
                UserId = default,
                EventId = Guid.NewGuid(),
                Text = "Text",
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(comment));
        }

        [Test]
        public void Create_WrongEventId_returnFalse()
        {
            CommentDto comment = new CommentDto()
            {
                Id = commentId,
                UserId = userId,
                EventId = Guid.NewGuid(),
                Text = "Text",
            };

            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Create(comment));
        }
    }
}
