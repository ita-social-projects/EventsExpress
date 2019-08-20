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

        Task<ChatRoom> GetChat(Guid chatId, Guid sender);

        IEnumerable<ChatRoom> GetUserChats(Guid userId);
        List<string> GetChatUserIds(Guid chatId);
    }
}
