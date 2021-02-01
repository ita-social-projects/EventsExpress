using System;
using System.Collections.Generic;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public Guid EventId { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Guid? CommentsId { get; set; }

        public IEnumerable<CommentDto> Children { get; set; }
    }
}
