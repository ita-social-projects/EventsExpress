// unset

namespace EventsExpress.Core.DTOs
{
    using System;

    public class NotificationTemplateDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string MessageText { get; set; }
    }
}
