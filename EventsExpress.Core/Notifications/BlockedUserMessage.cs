using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Notifications
{
    public class BlockedUserMessage : INotification
    {
        public string Email { get; }

        public BlockedUserMessage (string email)
        {
            Email = email;
        }
    }
}
