using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.Notifications;
using EventsExpress.Hubs;
using MediatR;

namespace EventsExpress.NotificationHandlers
{
    public class UserCreatedHandler : INotificationHandler<UserCreatedNotification>
    {
        private readonly UsersHub _hub;

        public UserCreatedHandler(UsersHub hub)
        {
            _hub = hub;
        }

        public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            await _hub.SendCountOfUsersAsync();
        }
    }
}
