using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class BlockedAccountMessage : INotification
    {
        public BlockedAccountMessage(Account account)
        {
            Account = account;
        }

        public Account Account { get; }
    }
}
