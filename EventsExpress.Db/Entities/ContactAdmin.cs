using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class ContactAdmin : BaseEntity
    {
        public Guid? SenderId { get; set; }

        public User Sender { get; set; }

        public Guid? AssigneeId { get; set; }

        public User Assignee { get; set; }

        public Guid MessageId { get; set; }

        public string Email { get; set; }

        public ContactAdminReason Subject { get; set; }

        public string Title { get; set; }

        public string EmailBody { get; set; }

        public string ResolutionDetails { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public virtual ContactAdminStatus Status { get; set; }
    }
}
