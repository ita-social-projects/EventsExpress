using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Test.ServiceTests
{
        [TestFixture]
        class CommentServiceTests : TestInitializer
        {
            private CommentService service;
            private Comments comment;
            private User user;
            private Event even;

            private static Mock<IUserService> mockUserService;
           
            
            [SetUp]
            protected override void Initialize()
            {
                base.Initialize();
                mockUserService = new Mock<IUserService>();
                service = new CommentService(mockUnitOfWork.Object, mockMapper.Object);
                comment = new Comments() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb"), Text="Text", EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"), UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"), Date = new DateTime(2019, 08, 08) };
                user = new User() { Email = "aaa@gmail.com", Id = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4") };
                even = new Event() { Id = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464") };
            }

            [Test]
            public void Delete_Existing_returnTrue()
            {
                
                mockUnitOfWork.Setup(u => u.CommentsRepository.Get(new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb"))).Returns(new Comments() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb"), Text = "Text", EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"), UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"), Date = new DateTime(2019, 08, 08) });
                var rez = service.Delete(new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb"));

                Assert.IsTrue(rez.Result.Successed);
            }

            [Test]
            public void Delete_NotExisting_returnFalse()
            {
                mockUnitOfWork.Setup(u => u.CommentsRepository.Get(new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb"))).Returns(new Comments() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bb"), Text = "Text", EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"), UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"), Date = new DateTime(2019, 08, 08) });
                var rez = service.Delete(new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"));

                Assert.IsFalse(rez.Result.Successed);
            }

            [Test]
            public void Create_RigthData_returnTrue()
            {
                CommentDTO comment = new CommentDTO() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"), UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"), EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"), Text = "Text" };
                Comments com = new Comments() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"), UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"), EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"), Text = "Text" };
             
                mockUnitOfWork.Setup(u => u.UserRepository.Get(new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"))).Returns(new User() { Email = "aaa@gmail.com", Id = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4") });
                mockUnitOfWork.Setup(u => u.EventRepository.Get(new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"))).Returns( new Event() { Id = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464") });
                mockUnitOfWork.Setup(u => u.CommentsRepository.Insert(com));

                var rez = service.Create(comment);

                Assert.IsTrue(rez.Result.Successed);
            }

            [Test]
            public void Create_WrongUserId_returnFalse()
            {
                CommentDTO comment = new CommentDTO() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"), UserId = new Guid(), EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"), Text = "Text" };
                Comments com = new Comments() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"), UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"), EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"), Text = "Text" };

                mockUnitOfWork.Setup(u => u.UserRepository.Get(new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"))).Returns(new User() { Email = "aaa@gmail.com", Id = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4") });
                mockUnitOfWork.Setup(u => u.EventRepository.Get(new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"))).Returns(new Event() { Id = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464") });
                mockUnitOfWork.Setup(u => u.CommentsRepository.Insert(com));

                var rez = service.Create(comment);

                Assert.IsFalse(rez.Result.Successed);
            }

            [Test]
            public void Create_WrongEventId_returnFalse()
            {
            CommentDTO comment = new CommentDTO() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"), UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"), EventId = new Guid(), Text = "Text" };
            Comments com = new Comments() { Id = new Guid("93f0c600-9c1b-48b4-9606-08d7141a36bc"), UserId = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"), EventId = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"), Text = "Text" };

                mockUnitOfWork.Setup(u => u.UserRepository.Get(new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4"))).Returns(new User() { Email = "aaa@gmail.com", Id = new Guid("19824dd6-67bf-4a52-24a7-08d705fcf8d4") });
                mockUnitOfWork.Setup(u => u.EventRepository.Get(new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464"))).Returns(new Event() { Id = new Guid("d3994f53-1e0d-4eda-d0e8-08d70c4f9464") });
                mockUnitOfWork.Setup(u => u.CommentsRepository.Insert(com));

                var rez = service.Create(comment);

                Assert.IsFalse(rez.Result.Successed);
            }




    }
}
