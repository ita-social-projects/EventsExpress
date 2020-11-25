using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Developer : BaseEntity
    {
        public string Name { get; set; }

        public Photo Photo { get; set; }

        public string Description { get; set; }

        public Guid TeamId { get; set; }

        public Team Team { get; set; }
    }
}
