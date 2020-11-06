using System;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class DenyParticipantMessage : INotification
    {
        public DenyParticipantMessage(Guid userId, Guid id)
        {
            UserId = userId;
            Id = id;
        }

        public Guid UserId { get; }

        public Guid Id { get; }
    }
}
