using System;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class ApproveParticipantMessage : INotification
    {
        public ApproveParticipantMessage(Guid userId, Guid id)
        {
            UserId = userId;
            Id = id;
        }

        public Guid UserId { get; }

        public Guid Id { get; }
    }
}
