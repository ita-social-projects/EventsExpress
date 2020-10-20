using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class BlockedUserMessage : INotification
    {
        public BlockedUserMessage(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
