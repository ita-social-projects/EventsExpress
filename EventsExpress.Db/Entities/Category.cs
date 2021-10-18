using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsExpress.Db.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public Guid CategoryGroupId { get; set; }

        public virtual CategoryGroup CategoryGroup { get; set; }

        public virtual IEnumerable<UserCategory> Users { get; set; }

        public virtual IEnumerable<EventCategory> Events { get; set; }
    }
}
