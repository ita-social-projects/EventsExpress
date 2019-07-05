using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual IEnumerable<UserCategory> Users { get; set; }
        public virtual IEnumerable<EventCategory> Events { get; set; }
    }
}
