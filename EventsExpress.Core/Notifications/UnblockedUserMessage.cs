using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Notifications
{
    public class UnblockedUserMessage : INotification
    {
        public string Email { get; }

        public UnblockedUserMessage(string email)
        {
            Email = email;
        }
    }
}
