// unset

namespace EventsExpress.Core.DTOs
{
    using System;

    public class NotificationTemplateDto
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string MessageText { get; set; }
    }
}
