using EventsExpress.Db.Entities;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class RegisterVerificationMessage : INotification
    {
        public RegisterVerificationMessage(AuthLocal auth)
        {
            AuthLocal = auth;
        }

        public AuthLocal AuthLocal { get; }
    }
}
