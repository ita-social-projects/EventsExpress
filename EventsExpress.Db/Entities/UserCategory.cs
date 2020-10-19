using System;

namespace EventsExpress.Db.Entities
{
    public class UserCategory
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
