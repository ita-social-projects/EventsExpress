using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class MessageMapperProfileTests : MapperTestInitializer<MessageMapperProfile>
    {
        private static Guid chatId = Guid.NewGuid();
        private static Guid userId = Guid.NewGuid();
        private static User user = new User
        {
            Id = userId,
            Name = "user",
            Birthday = new DateTime(2001, 01, 01),
        };

        private ChatRoom chatRoom = new ChatRoom
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

        [OneTimeSetUp]
        protected virtual void Init()
        {
            Initialize();

            IServiceCollection services = new ServiceCollection();
            var mock = new Mock<IPhotoService>();
            services.AddTransient<IPhotoService>(sp => mock.Object);

            services.AddAutoMapper(typeof(MessageMapperProfile));

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Mapper = serviceProvider.GetService<IMapper>();
            mock.Setup(x => x.GetPhotoFromAzureBlob(It.IsAny<string>())).Returns(Task.FromResult("test"));
        }

        [Test]
        public void MessageMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void MessageMapperProfile_ChatRoomToUserChatViewModel()
        {
            var res = Mapper.Map<ChatRoom, UserChatViewModel>(chatRoom);

            Assert.That(res.Id, Is.EqualTo(chatRoom.Id));
            Assert.That(res.Title, Is.EqualTo(chatRoom.Title));
            Assert.That(res.LastMessage, Is.EqualTo(chatRoom.Messages.LastOrDefault().Text));
            Assert.That(res.LastMessageTime, Is.EqualTo(chatRoom.Messages.LastOrDefault().DateCreated));
            Assert.That(res.Users, Has.All.Matches<UserPreviewViewModel>(x => chatRoom.Users
                .All(u =>
                    x.Id == u.UserId &&
                    x.Birthday == u.User.Birthday &&
                    x.Username == u.User.Name &&
                    x.PhotoUrl == "test")));
            Assert.That(res.Id, Is.EqualTo(chatRoom.Id));
        }

        [Test]
        public void MessageMapperProfile_ChatRoomToChatViewModel()
        {
            var res = Mapper.Map<ChatRoom, ChatViewModel>(chatRoom);

            Assert.That(res.Id, Is.EqualTo(chatRoom.Id));
            Assert.That(res.Title, Is.EqualTo(chatRoom.Title));
            Assert.That(res.Messages, Has.All.Matches<MessageViewModel>(x => chatRoom.Messages
                .All(m =>
                    x.Id == m.Id &&
                    x.ChatRoomId == m.ChatRoomId &&
                    x.DateCreated == m.DateCreated &&
                    x.SenderId == m.SenderId &&
                    x.Seen == m.Seen &&
                    x.Edited == m.Edited &&
                    x.Text == m.Text)));
            Assert.That(res.Users, Has.All.Matches<UserPreviewViewModel>(x => chatRoom.Users
                .All(u =>
                    x.Id == u.UserId &&
                    x.Birthday == u.User.Birthday &&
                    x.Username == u.User.Name &&
                    x.PhotoUrl == "test")));
            Assert.That(res.Id, Is.EqualTo(chatRoom.Id));
        }
    }
}
