using EventsExpress.Core.Infrastructure;
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

        List<Message> GetUnreadMessages(Guid userId);

        Task<OperationResult> MsgSeen(List<Guid> messageIds);
        IEnumerable<ChatRoom> GetUserChats(Guid userId);
        List<string> GetChatUserIds(Guid chatId);
    }
}
