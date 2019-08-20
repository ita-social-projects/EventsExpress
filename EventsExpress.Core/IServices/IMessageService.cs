using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IMessageService
    {
        Task<Message> Send(Guid chatId, Guid Sender, string Text);

        IEnumerable<ChatRoom> GetUserChats(Guid userId);
        ChatRoom GetChat(Guid chatId);
    }
}
