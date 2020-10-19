using System;

namespace EventsExpress.Db.Entities
{
    public class Message : BaseEntity
    {
        public Guid ChatRoomId { get; set; }

        public Guid SenderId { get; set; }

        public User Sender { get; set; }

        public Guid? ParentId { get; set; }

        public Message Parent { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public bool Edited { get; set; } = false;

        public bool Seen { get; set; } = false;

        public string Text { get; set; }
    }
}
