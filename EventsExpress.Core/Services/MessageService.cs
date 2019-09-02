using AutoMapper;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class MessageService : IMessageService
    {

        private readonly IUnitOfWork Db;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork uow, IMapper mapper)
        {
            Db = uow;
            _mapper = mapper;
        }                             

        public IEnumerable<ChatRoom> GetUserChats(Guid userId)
        {
            var res = Db.ChatRepository
                .Get("Users.User.Photo,Messages")
                .Where(x => x.Users.Any(u => u.UserId == userId));
            return res.AsEnumerable();
        }

        public async Task<ChatRoom> GetChat(Guid chatId, Guid sender)
        {

            var chat = Db.ChatRepository.Get("")
                .FirstOrDefault(x => x.Id == chatId) ?? 
                Db.ChatRepository.Get("")
                .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            if (chat == null)           
            { 
                chat = new ChatRoom();
                chat.Users = new List<UserChat>(); 
                chat.Users.Add(new UserChat { Chat = chat, UserId = sender });
                chat.Users.Add(new UserChat { Chat = chat, UserId = chatId });
                chat = Db.ChatRepository.Insert(chat);
                await Db.SaveAsync(); 
            }

            var res = Db.ChatRepository
                .Get("Users.User.Photo,Messages")
                .FirstOrDefault(x => x.Id == chat.Id);     
            return res;
        }
           
        public async Task<Message> Send(Guid chatId, Guid sender, string text)
        {
            var chat = Db.ChatRepository.Get(chatId);
            if (chat == null)
            {
                chat = Db.ChatRepository.Get("")
                    .FirstOrDefault(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
            }

            var msg = Db.MessageRepository.Insert(new Message { ChatRoomId = chat.Id, SenderId = sender, Text = text });
            await Db.SaveAsync();
            return msg;
        }

        public List<string> GetChatUserIds(Guid chatId)
        {
            return Db.ChatRepository.Get("Users").FirstOrDefault(x => x.Id == chatId).Users.Select(y => y.UserId.ToString()).ToList();
        }

        public async Task<OperationResult> MsgSeen(List<Guid> messageIds)
        {
            foreach(var x in messageIds)
            {
                var msg = Db.MessageRepository.Get(x);
                if(msg == null)
                {                          
                    return new OperationResult(false, "Msg not found", "");
                }
                msg.Seen = true;
                await Db.SaveAsync();
            }         
            return new OperationResult(true, "", Db.MessageRepository.Get(messageIds[0]).ChatRoomId.ToString());
        }

        public List<Message> GetUnreadMessages(Guid userId)
        {
            var chats = GetUserChats(userId).Select(y => y.Id).ToList();
            return Db.MessageRepository.Get("").Where(x => chats.Contains(x.ChatRoomId) && x.SenderId != userId && !x.Seen).ToList();

        }
    }
}
