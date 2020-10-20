using System;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class BlockedEventMessage : INotification
    {
        public BlockedEventMessage(Guid userId, Guid id)
        {
            UserId = userId;
            Id = id;
        }

        public Guid UserId { get; }

        public Guid Id { get; }
    }
}
