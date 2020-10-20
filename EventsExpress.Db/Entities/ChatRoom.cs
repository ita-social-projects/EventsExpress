using System.Collections.Generic;

namespace EventsExpress.Db.Entities
{
    public class ChatRoom : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<UserChat> Users { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
