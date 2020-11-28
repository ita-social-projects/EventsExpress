using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
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
                Name = "NameIsExist",
                Email = "user@gmail.com",
            };

            evnt = new Event
            {
                Id = eventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "...",
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
            var test = service.Delete(commentId);

            Assert.IsTrue(test.Result.Successed);
        }

        [Test]
        public void Delete_NotExisting_returnFalse()
        {
            var test = service.Delete(Guid.NewGuid());

            Assert.IsFalse(test.Result.Successed);
        }

        [Test]
        public void Create_RigthData_returnTrue()
        {
            CommentDTO comment = new CommentDTO()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                User = user,
                Text = "Text",
            };

            var test = service.Create(comment);

            Assert.IsTrue(test.Result.Successed);
        }

        [Test]
        public void Create_WrongUserId_returnFalse()
        {
            CommentDTO comment = new CommentDTO()
            {
                Id = commentId,
                UserId = default,
                EventId = Guid.NewGuid(),
                Text = "Text",
            };

            var test = service.Create(comment);

            Assert.IsFalse(test.Result.Successed);
        }

        [Test]
        public void Create_WrongEventId_returnFalse()
        {
            CommentDTO comment = new CommentDTO()
            {
                Id = commentId,
                UserId = userId,
                EventId = default,
                Text = "Text",
            };

            var test = service.Create(comment);

            Assert.IsFalse(test.Result.Successed);
        }
    }
}
