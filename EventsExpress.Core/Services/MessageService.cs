using AutoMapper;                 
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
                .Get("Users.User.Photo")
                .Where(x => x.Users.Any(u => u.UserId == userId));
            return res.AsEnumerable();
        }

        public ChatRoom GetChat(Guid chatId)
        {
            var res = Db.ChatRepository
                .Get("Users.User.Photo,Messages")
                .FirstOrDefault(x => x.Id == chatId);
            return res;
        }


        public async Task<Message> Send(Guid chatId, Guid Sender, string Text)
        {
            var msg = Db.MessageRepository.Insert(new Message { ChatRoomId = chatId, SenderId = Sender, Text = Text });
            await Db.SaveAsync();
            return msg;
        }
    }
}
