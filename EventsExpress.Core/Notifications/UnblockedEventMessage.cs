using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Notifications
{
    public class UnblockedEventMessage : INotification
    {
        public Guid UserId { get; }
        public Guid Id { get; }

        public UnblockedEventMessage(Guid userId, Guid id)
        {
            UserId = userId;
            Id = id;
        }
    }
}
