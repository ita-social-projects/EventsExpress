using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class Report : BaseEntity
    {
        public string Description { get; set; }

        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
