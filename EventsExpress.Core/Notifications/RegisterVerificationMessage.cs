using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class RegisterVerificationMessage : INotification
    {
        public RegisterVerificationMessage(UserDTO userDto)
        {
            User = userDto;
        }

        public UserDTO User { get; }
    }
}
