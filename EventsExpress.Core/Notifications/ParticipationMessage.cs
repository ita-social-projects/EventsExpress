using System;
using EventsExpress.Db.Enums;
using MediatR;

namespace EventsExpress.Core.Notifications
{
    public class ParticipationMessage : INotification
    {
        public ParticipationMessage(Guid userId, Guid id, UserStatusEvent status)
        {
            UserId = userId;
            Id = id;
            Status = status;
        }

        public Guid UserId { get; }

        public Guid Id { get; }

        public UserStatusEvent Status { get; set; }
    }
}
