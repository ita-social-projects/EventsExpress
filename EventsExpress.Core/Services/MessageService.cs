using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork db;

        public MessageService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<ChatRoom> GetUserChats(Guid userId)
        {
            var res = db.ChatRepository
                .Get("Users.User.Photo,Messages")
                .Where(x => x.Users.Any(u => u.UserId == userId));
            return res.AsEnumerable();
        }

        public async Task<ChatRoom> GetChat(Guid chatId, Guid sender)
        {
            var chat = db.ChatRepository.Get(string.Empty)
                .FirstOrDefault(x => x.Id == chatId) ??
                db.ChatRepository.Get(string.Empty)
                .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            if (chat == null)
            {
                chat = new ChatRoom();
                chat.Users = new List<UserChat>
                {
                    new UserChat { Chat = chat, UserId = sender },
                    new UserChat { Chat = chat, UserId = chatId },
                };
                chat = db.ChatRepository.Insert(chat);
                await db.SaveAsync();
            }

            var res = db.ChatRepository
                .Get("Users.User.Photo,Messages")
                .FirstOrDefault(x => x.Id == chat.Id);
            return res;
        }

        public async Task<Message> Send(Guid chatId, Guid sender, string text)
        {
            var chat = db.ChatRepository.Get(chatId);
            if (chat == null)
            {
                chat = db.ChatRepository.Get(string.Empty)
                    .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            }

            var msg = db.MessageRepository.Insert(new Message { ChatRoomId = chat.Id, SenderId = sender, Text = text });
            await db.SaveAsync();
            return msg;
        }

        public List<string> GetChatUserIds(Guid chatId)
        {
            return db.ChatRepository.Get("Users").FirstOrDefault(x => x.Id == chatId).Users.Select(y => y.UserId.ToString()).ToList();
        }

        public async Task<OperationResult> MsgSeen(List<Guid> messageIds)
        {
            foreach (var x in messageIds)
            {
                var msg = db.MessageRepository.Get(x);
                if (msg == null)
                {
                    return new OperationResult(false, "Msg not found", string.Empty);
                }

                msg.Seen = true;
                await db.SaveAsync();
            }

            return new OperationResult(true, string.Empty, db.MessageRepository.Get(messageIds[0]).ChatRoomId.ToString());
        }

        public List<Message> GetUnreadMessages(Guid userId)
        {
            var chats = GetUserChats(userId).Select(y => y.Id).ToList();
            return db.MessageRepository.Get(string.Empty).Where(x => chats.Contains(x.ChatRoomId) && x.SenderId != userId && !x.Seen).ToList();
        }
    }
}
