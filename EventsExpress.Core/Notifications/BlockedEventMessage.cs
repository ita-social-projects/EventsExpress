using System;
using System.Collections.Generic;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class BlockedEventMessage : INotification
    {
        public BlockedEventMessage(IEnumerable<Guid> userId, Guid id)
        {
            UserId = userId;
            Id = id;
        }

        public IEnumerable<Guid> UserId { get; }

        public Guid Id { get; }
    }
}
