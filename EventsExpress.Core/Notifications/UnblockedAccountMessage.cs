using EventsExpress.Core.NotificationModels;
using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class UnblockedAccountMessage : INotification
    {
        public UnblockedAccountMessage(Account account)
        {
            Account = account;
            Model = new UnblockedAccountNotificationModel();
        }

        public Account Account { get; }

        public UnblockedAccountNotificationModel Model { get; }
    }
}
