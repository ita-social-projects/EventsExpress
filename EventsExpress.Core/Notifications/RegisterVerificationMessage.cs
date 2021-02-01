using EventsExpress.Core.DTOs;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class RegisterVerificationMessage : INotification
    {
        public RegisterVerificationMessage(UserDto userDto)
        {
            User = userDto;
        }

        public UserDto User { get; }
    }
}
