using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        private readonly AppDbContext _context;

        public MessageService(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<ChatRoom> GetUserChats(Guid userId)
        {
            var res = _context.ChatRoom
                .Include("Users.User.Photo")
                .Include("Messages")
                .Where(x => x.Users.Any(u => u.UserId == userId));
            return res.AsEnumerable();
        }

        public async Task<ChatRoom> GetChat(Guid chatId, Guid sender)
        {
            var chat = _context.ChatRoom
                .FirstOrDefault(x => x.Id == chatId) ??
                _context.ChatRoom
                .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            if (chat == null)
            {
                chat = new ChatRoom();
                chat.Users = new List<UserChat>
                {
                    new UserChat { Chat = chat, UserId = sender },
                    new UserChat { Chat = chat, UserId = chatId },
                };
                _context.ChatRoom.Add(chat);
                await _context.SaveChangesAsync();
            }

            var res = _context.ChatRoom
                .Include("Users.User.Photo")
                .Include("Messages")
                .FirstOrDefault(x => x.Id == chat.Id);
            return res;
        }

        public async Task<Message> Send(Guid chatId, Guid sender, string text)
        {
            var chat = _context.ChatRoom.Find(chatId);
            if (chat == null)
            {
                chat = _context.ChatRoom
                    .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            }

            var msg = Insert(new Message { ChatRoomId = chat.Id, SenderId = sender, Text = text });
            await _context.SaveChangesAsync();
            return msg;
        }

        public List<string> GetChatUserIds(Guid chatId)
        {
            return _context.ChatRoom
                .Include("Users")
                .FirstOrDefault(x => x.Id == chatId).Users.Select(y => y.UserId.ToString()).ToList();
        }

        public async Task<OperationResult> MsgSeen(List<Guid> messageIds)
        {
            foreach (var x in messageIds)
            {
                var msg = Get(x);
                if (msg == null)
                {
                    return new OperationResult(false, "Msg not found", string.Empty);
                }

                msg.Seen = true;
                await _context.SaveChangesAsync();
            }

            return new OperationResult(true, string.Empty, Get(messageIds[0]).ChatRoomId.ToString());
        }

        public List<Message> GetUnreadMessages(Guid userId)
        {
            var chats = GetUserChats(userId).Select(y => y.Id).ToList();
            return Get(string.Empty).Where(x => chats.Contains(x.ChatRoomId) && x.SenderId != userId && !x.Seen).ToList();
        }
    }
}
