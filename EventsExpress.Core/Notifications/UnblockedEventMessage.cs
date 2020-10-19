using System;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class UnblockedEventMessage : INotification
    {
        public UnblockedEventMessage(Guid userId, Guid id)
        {
            UserId = userId;
            Id = id;
        }

        public Guid UserId { get; }

        public Guid Id { get; }
    }
}
