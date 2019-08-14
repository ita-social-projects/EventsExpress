using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Core.DTOs;
using MediatR;


namespace EventsExpress.Core.Notifications
{
    public class RegisterVerificationMessage: INotification
    {
        public UserDTO User { get; }

        public RegisterVerificationMessage(UserDTO userDto)
        {            
            User = userDto;
        }
    }
}
