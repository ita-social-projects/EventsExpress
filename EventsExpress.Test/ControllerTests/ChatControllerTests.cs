using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Controllers;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ControllerTests
{
    [TestFixture]
    public class ChatControllerTests : TestInitializer
    {
        private static Guid chatId = Guid.NewGuid();
        private static Guid userId = Guid.NewGuid();
        private static User user = new User
        {
            Id = userId,
            Name = "user",
            Birthday = new DateTime(2001, 01, 01),
        };

        private static ChatRoom chat = new ChatRoom
        {
            Id = chatId,
            Title = "title",
            Users = new List<UserChat>
                {
                    new UserChat
                    {
                        Id = Guid.NewGuid(),
                        User = user,
                        UserId = user.Id,
                        ChatId = chatId,
                    },
                },
            Messages = new List<Message>
                {
                    new Message
                    {
                        Id = Guid.NewGuid(),
                        Text = "message",
                        DateCreated = new DateTime(2021, 03, 24),
                        ChatRoomId = Guid.NewGuid(),
                        SenderId = userId,
                        Sender = user,
                    },
                },
        };

        private ChatController chatController;
        private Mock<IMapper> mockMapper;
        private Mock<IMessageService> mockMessageService;
        private Mock<ISecurityContext> mockSecurityContextService;

        private IEnumerable<ChatRoom> userChats = new List<ChatRoom>()
        {
            chat,
        };

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            mockMapper = new Mock<IMapper>();
            mockMessageService = new Mock<IMessageService>();
            mockSecurityContextService = new Mock<ISecurityContext>();

            chatController = new ChatController(
                mockMessageService.Object,
                mockMapper.Object,
                mockSecurityContextService.Object);

            mockSecurityContextService.Setup(x => x.GetCurrentUserId()).Returns(userId);
        }

        [Test]
        public void GetAll_ReturnsOk()
        {
            var res = chatController.All();

            Assert.IsInstanceOf<OkObjectResult>(res);
        }

        [Test]
        public void GetChat_ReturnsOk()
        {
            mockSecurityContextService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            mockMessageService.Setup(x => x.GetChat(chatId, userId)).Returns(Task.FromResult(chat));

            var res = chatController.GetChat(chatId);
            var okRes = res.Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(okRes);
        }

        [Test]
        public void GetChat_ReturnsBadRequest()
        {
            mockSecurityContextService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            mockMessageService.Setup(x => x.GetChat(chatId, userId)).Returns(Task.FromResult((ChatRoom)null));

            var res = chatController.GetChat(chatId);
            var badRes = res.Result as BadRequestResult;

            Assert.IsInstanceOf<BadRequestResult>(badRes);
        }
    }
}
