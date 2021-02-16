using System;
using System.Collections.Generic;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class BlockedEventMessage : INotification
    {
        public BlockedEventMessage(IEnumerable<Guid> userId, Guid id)
        {
            UserIds = userId;
            Id = id;
        }

        public IEnumerable<Guid> UserIds { get; }

        public Guid Id { get; set; }
    }
}
