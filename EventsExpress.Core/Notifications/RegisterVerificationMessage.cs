using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Core.DTOs;
using MediatR;


namespace EventsExpress.Core.Notifications
{
    public class RegisterVerificationMessage: INotification
    {
       // public string Message { get; }
        public UserDTO User { get; }

        public RegisterVerificationMessage(UserDTO userDto)
        {
           // Message=$"new user was registred: id{userDto.Id}" ;
            User = userDto;

        }
    }
}
