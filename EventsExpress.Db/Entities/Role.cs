using System.Collections.Generic;

namespace EventsExpress.Db.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
