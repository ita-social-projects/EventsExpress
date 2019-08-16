using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.DTO
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public Guid EventId { get; set; }

        public DateTime Date { get; set; }

        public string UserPhoto { get; set; }

        public string UserName { get; set; }

        public Guid? CommentsId { get; set; }

        public IEnumerable<CommentDto> Children { get; set; }
    }
}
