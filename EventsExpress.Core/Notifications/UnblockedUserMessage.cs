using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class UnblockedUserMessage : INotification
    {
        public UnblockedUserMessage(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
