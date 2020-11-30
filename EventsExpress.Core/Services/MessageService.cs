using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _db;

        public MessageService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public IEnumerable<ChatRoom> GetUserChats(Guid userId)
        {
            var res = _db.ChatRepository
                .Get("Users.User.Photo,Messages")
                .Where(x => x.Users.Any(u => u.UserId == userId));
            return res.AsEnumerable();
        }

        public async Task<ChatRoom> GetChat(Guid chatId, Guid sender)
        {
            var chat = _db.ChatRepository.Get(string.Empty)
                .FirstOrDefault(x => x.Id == chatId) ??
                _db.ChatRepository.Get(string.Empty)
                .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            if (chat == null)
            {
                chat = new ChatRoom();
                chat.Users = new List<UserChat>
                {
                    new UserChat { Chat = chat, UserId = sender },
                    new UserChat { Chat = chat, UserId = chatId },
                };
                chat = _db.ChatRepository.Insert(chat);
                await _db.SaveAsync();
            }

            var res = _db.ChatRepository
                .Get("Users.User.Photo,Messages")
                .FirstOrDefault(x => x.Id == chat.Id);
            return res;
        }

        public async Task<Message> Send(Guid chatId, Guid sender, string text)
        {
            var chat = _db.ChatRepository.Get(chatId);
            if (chat == null)
            {
                chat = _db.ChatRepository.Get(string.Empty)
                    .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            }

            var msg = _db.MessageRepository.Insert(new Message { ChatRoomId = chat.Id, SenderId = sender, Text = text });
            await _db.SaveAsync();
            return msg;
        }

        public List<string> GetChatUserIds(Guid chatId)
        {
            return _db.ChatRepository.Get("Users").FirstOrDefault(x => x.Id == chatId).Users.Select(y => y.UserId.ToString()).ToList();
        }

        public async Task<Guid> MsgSeen(List<Guid> messageIds)
        {
            foreach (var x in messageIds)
            {
                var msg = _db.MessageRepository.Get(x);
                if (msg == null)
                {
                    throw new EventsExpressException("Msg not found");
                }

                msg.Seen = true;
                await _db.SaveAsync();
            }

            return _db.MessageRepository.Get(messageIds[0]).ChatRoomId;
        }

        public List<Message> GetUnreadMessages(Guid userId)
        {
            var chats = GetUserChats(userId).Select(y => y.Id).ToList();
            return _db.MessageRepository.Get(string.Empty).Where(x => chats.Contains(x.ChatRoomId) && x.SenderId != userId && !x.Seen).ToList();
        }
    }
}
