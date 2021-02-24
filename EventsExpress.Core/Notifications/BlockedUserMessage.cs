using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class BlockedUserMessage : INotification
    {
        public BlockedUserMessage(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}
