using AutoMapper;                 
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


        public void Send(Guid Receiver, string Text)
        {
            throw new NotImplementedException();
        }
    }
}
