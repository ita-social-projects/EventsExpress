using System.Collections.Generic;

namespace EventsExpress.Db.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual IEnumerable<UserCategory> Users { get; set; }

        public virtual IEnumerable<EventCategory> Events { get; set; }
    }
}
