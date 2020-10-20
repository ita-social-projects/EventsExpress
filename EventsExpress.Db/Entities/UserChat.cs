using System;

namespace EventsExpress.Db.Entities
{
    public class UserChat : BaseEntity
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid ChatId { get; set; }

        public virtual ChatRoom Chat { get; set; }
    }
}
