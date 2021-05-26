using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class ContactAdminViewModel
    {
        public string Description { get; set; }

        public ContactAdminReason Subject { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public Guid MessageId { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public ContactAdminStatus Status { get; set; }
    }
}
