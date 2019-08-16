using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Message : BaseEntity
    {
        public Guid ChatId { get; set; }    

        public Guid SenderId { get; set; }
        public User Sender { get; set; }

        public Guid? ParentId { get; set; }
        public Message Parent { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool Edited { get; set; } = false;

        public string Text { get; set; }

    }
}
