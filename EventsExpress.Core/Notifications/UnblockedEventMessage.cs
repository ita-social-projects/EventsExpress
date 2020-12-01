using System;
using System.Collections.Generic;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class UnblockedEventMessage : INotification
    {
        public UnblockedEventMessage(IEnumerable<Guid> userId, Guid id)
        {
            UserId = userId;
            Id = id;
        }

        public IEnumerable<Guid> UserId { get; }

        public Guid Id { get; }
    }
}
