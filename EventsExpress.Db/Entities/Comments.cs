using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Comments : BaseEntity
    {
        public string Text { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }

        public Guid? CommentsId { get; set; }
       
        public virtual IEnumerable<Comments> Children { get; set; }

        public DateTime Date { get; set; }
        public Comments Parent { get; set; }
    }
}
