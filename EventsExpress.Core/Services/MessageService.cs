using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        public MessageService(AppDbContext context)
            : base(context)
        {
        }

        public IEnumerable<ChatRoom> GetUserChats(Guid userId)
        {
            var res = Context.ChatRoom
                .Include(c => c.Users)
                    .ThenInclude(u => u.User)
                .Include(c => c.Messages)
                .Where(x => x.Users.Any(u => u.UserId == userId));
            return res.AsEnumerable();
        }

        public async Task<ChatRoom> GetChat(Guid chatId, Guid sender)
        {
            var chat = Context.ChatRoom
                .FirstOrDefault(x => x.Id == chatId) ??
                Context.ChatRoom
                .FirstOrDefault(x =>
                    x.Users.Count == 2 &&
                    x.Users.Any(y => y.UserId == chatId) &&
                    x.Users.Any(y => y.UserId == sender));

            if (chat == null)
            {
                chat = new ChatRoom();
                chat.Users = new List<UserChat>
                {
                    new UserChat { Chat = chat, UserId = sender },
                    new UserChat { Chat = chat, UserId = chatId },
                };
                Context.ChatRoom.Add(chat);
                await Context.SaveChangesAsync();
            }

            var res = Context.ChatRoom
                .Include(c => c.Users)
                    .ThenInclude(u => u.User)
                .Include(c => c.Messages)
                .FirstOrDefault(x => x.Id == chat.Id);

            return res;
        }

        public async Task<Message> Send(Guid chatId, Guid sender, string text)
        {
            var chat = Context.ChatRoom.Find(chatId);
            if (chat == null)
            {
                chat = Context.ChatRoom
                    .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            }

            var msg = Insert(new Message { ChatRoomId = chat.Id, SenderId = sender, Text = text });
            await Context.SaveChangesAsync();

            return msg;
        }

        public List<string> GetChatUserIds(Guid chatId)
        {
            return Context.ChatRoom
                .Include(c => c.Users)
                .FirstOrDefault(x => x.Id == chatId).Users.Select(y => y.UserId.ToString()).ToList();
        }

        public async Task<Guid> MsgSeen(List<Guid> messageIds)
        {
            foreach (var x in messageIds)
            {
                var msg = Context.Message.Find(x);
                if (msg == null)
                {
                    throw new EventsExpressException("Msg not found");
                }

                msg.Seen = true;
                await Context.SaveChangesAsync();
            }

            return Context.Message.Find(messageIds[0]).ChatRoomId;
        }

        public List<Message> GetUnreadMessages(Guid userId)
        {
            var chats = GetUserChats(userId).Select(y => y.Id).ToList();

            return Context.Message
                .Where(x => chats
                    .Contains(x.ChatRoomId) && x.SenderId != userId && !x.Seen)
                .ToList();
        }
    }
}
