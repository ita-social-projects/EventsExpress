using System;
using System.Collections.Generic;
using System.Linq;
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

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            service = new CommentService(MockUnitOfWork.Object, MockMapper.Object);

            existingComment = new Comments
            {
                Text = "Test comment",
                Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb"),
                Date = new DateTime(2019, 08, 08),
                EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"),
                UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"),
            };

            MockUnitOfWork.Setup(u => u.CommentsRepository
                .Get(It.IsAny<string>()))
                .Returns(new List<Comments> { existingComment }.AsQueryable());

            MockUnitOfWork.Setup(u => u.CommentsRepository
                .Get(new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb")))
                .Returns(existingComment);
        }

        [Test]
        public void Delete_Existing_returnTrue()
        {
            var test = service.Delete(new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb"));

            Assert.IsTrue(test.Result.Successed);
        }

        [Test]
        public void Delete_NotExisting_returnFalse()
        {
            var test = service.Delete(new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"));

            Assert.IsFalse(test.Result.Successed);
        }

        [Test]
        public void Create_RigthData_returnTrue()
        {
            CommentDTO comment = new CommentDTO()
            {
                Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"),
                UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"),
                EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"),
                Text = "Text",
            };
            Comments com = new Comments()
            {
                Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"),
                UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"),
                EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"),
                Text = "Text",
            };

            MockUnitOfWork.Setup(u => u.UserRepository
                .Get(new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4")))
                .Returns(new User()
                {
                    Email = "aaa@gmail.com",
                    Id = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"),
                });
            MockUnitOfWork.Setup(u => u.EventRepository.
                Get(new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464")))
                .Returns(new Event() { Id = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464") });

            MockUnitOfWork.Setup(u => u.CommentsRepository.Insert(com));

            var test = service.Create(comment);

            Assert.IsTrue(test.Result.Successed);
        }

        [Test]
        public void Create_WrongUserId_returnFalse()
        {
            CommentDTO comment = new CommentDTO()
            {
                Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"),
                UserId = default, 
                EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"),
                Text = "Text",
            };
            Comments com = new Comments()
            {
                Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"),
                UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"),
                EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"),
                Text = "Text",
            };

            MockUnitOfWork.Setup(u => u.UserRepository
                .Get(new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4")))
                .Returns(new User() { Email = "aaa@gmail.com", Id = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4") });

            MockUnitOfWork.Setup(u => u.EventRepository
                .Get(new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464")))
                .Returns(new Event() { Id = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464") });

            MockUnitOfWork.Setup(u => u.CommentsRepository.Insert(com));

            var test = service.Create(comment);

            Assert.IsFalse(test.Result.Successed);
        }

        [Test]
        public void Create_WrongEventId_returnFalse()
        {
            CommentDTO comment = new CommentDTO()
            {
                Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"),
                UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"),
                EventId = default, Text = "Text",
            };
            Comments com = new Comments()
            {
                Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"),
                UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"),
                EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"),
                Text = "Text",
            };

            MockUnitOfWork.Setup(u => u.UserRepository
                .Get(new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4")))
                .Returns(new User() { Email = "aaa@gmail.com", Id = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4") });

            MockUnitOfWork.Setup(u => u.EventRepository
                .Get(new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464")))
                .Returns(new Event() { Id = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464") });

            MockUnitOfWork.Setup(u => u.CommentsRepository.Insert(com));

            var test = service.Create(comment);

            Assert.IsFalse(test.Result.Successed);
        }
    }
}
