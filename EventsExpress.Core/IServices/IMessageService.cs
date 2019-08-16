using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.IServices
{
    public interface IMessageService
    {
        void Send(Guid Receiver, string Text);

        IEnumerable<ChatRoom> GetUserChats(Guid userId);
        ChatRoom GetChat(Guid chatId);
    }
}
