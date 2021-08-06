using EventsExpress.Core.NotificationModels;
using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class RegisterVerificationMessage : INotification
    {
        public RegisterVerificationMessage(AuthLocal auth)
        {
            AuthLocal = auth;
            Model = new RegisterVerificationNotificationModel();
        }

        public AuthLocal AuthLocal { get; }

        public RegisterVerificationNotificationModel Model { get; }
    }
}
