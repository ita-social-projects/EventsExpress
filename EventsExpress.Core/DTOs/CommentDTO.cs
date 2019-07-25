using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public Guid EventId { get; set; }

        public DateTime Date { get; set; }
    }
}
