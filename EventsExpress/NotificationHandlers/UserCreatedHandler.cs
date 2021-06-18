using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Enums;
using EventsExpress.Hubs;
using MediatR;

namespace EventsExpress.NotificationHandlers
{
    public class UserCreatedHandler : INotificationHandler<UserCreatedMessage>
    {
        private readonly UsersHub _hub;

        public UserCreatedHandler(UsersHub hub)
        {
            _hub = hub;
        }

        public async Task Handle(UserCreatedMessage notification, CancellationToken cancellationToken)
        {
            await _hub.SendCountOfUsersAsync(AccountStatus.All);
        }
    }
}
