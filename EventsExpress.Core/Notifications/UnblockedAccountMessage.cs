using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class UnblockedAccountMessage : INotification
    {
        public UnblockedAccountMessage(Account account)
        {
            Account = account;
        }

        public Account Account { get; }
    }
}
