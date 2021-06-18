using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Hubs;
using MediatR;

namespace EventsExpress.NotificationHandlers
{
    public class UserRoleChangedHandler : INotificationHandler<UserRoleChangedMessage>
    {
        private readonly ICacheHelper _cacheHelper;

        public UserRoleChangedHandler(ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }

        public Task Handle(UserRoleChangedMessage notification, CancellationToken cancellationToken)
        {
            _cacheHelper.Delete(UsersHub.AdminsCacheKey);

            return Task.CompletedTask;
        }
    }
}
