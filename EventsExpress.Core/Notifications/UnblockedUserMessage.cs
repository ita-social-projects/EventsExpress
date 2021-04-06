using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class UnblockedUserMessage : INotification
    {
        public UnblockedUserMessage(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}
