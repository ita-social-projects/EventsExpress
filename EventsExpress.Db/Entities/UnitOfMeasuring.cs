using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class UnitOfMeasuring : BaseEntity
    {
        public string UnitName { get; set; }

        public string ShortName { get; set; }

        public ICollection<Inventory> Inventories { get; set; }

        public CategoryOfMeasuring Category { get; set; }

        public Guid CategoryId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
