using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface IMessageService
    {
        Task<Message> Send(Guid chatId, Guid sender, string text);

        Task<ChatRoom> GetChat(Guid chatId, Guid sender);

        List<Message> GetUnreadMessages(Guid userId);

        Task<OperationResult> MsgSeen(List<Guid> messageIds);

        IEnumerable<ChatRoom> GetUserChats(Guid userId);

        List<string> GetChatUserIds(Guid chatId);
    }
}
