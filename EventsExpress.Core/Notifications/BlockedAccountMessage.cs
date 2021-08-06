using EventsExpress.Core.NotificationModels;
using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class BlockedAccountMessage : INotification
    {
        public BlockedAccountMessage(Account account)
        {
            Account = account;
            Model = new BlockedAccountNotificationModel();
        }

        public Account Account { get; }

        public BlockedAccountNotificationModel Model { get; }
    }
}
