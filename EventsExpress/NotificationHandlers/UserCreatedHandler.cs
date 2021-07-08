using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.Notifications;
using EventsExpress.Hubs;
using EventsExpress.Hubs.Clients;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.NotificationHandlers
{
    public class UserCreatedHandler : INotificationHandler<UserCreatedMessage>
    {
        private readonly IHubContext<UsersHub, IUsersClient> _usersHubContext;

        public UserCreatedHandler(IHubContext<UsersHub, IUsersClient> usersHubContext)
        {
            _usersHubContext = usersHubContext;
        }

        public async Task Handle(UserCreatedMessage notification, CancellationToken cancellationToken)
        {
            await _usersHubContext.Clients.All.CountUsers();
        }
    }
}
