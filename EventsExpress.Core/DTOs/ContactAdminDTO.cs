using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class ContactAdminDto
    {
        public ContactAdminReason Subject { get; set; }

        public string MessageText { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }
    }
}
