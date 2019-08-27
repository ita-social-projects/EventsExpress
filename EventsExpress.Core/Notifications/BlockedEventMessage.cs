using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.Notifications
{
    public class BlockedEventMessage : INotification
    {
        public Guid UserId { get; }
        public Guid Id { get; }

        public BlockedEventMessage(Guid userId, Guid id)
        {
            UserId = userId;
            Id = id;
        }
    }
}
