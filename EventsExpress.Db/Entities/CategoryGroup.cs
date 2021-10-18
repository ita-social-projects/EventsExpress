using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class CategoryGroup : BaseEntity
    {
        public string Title { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}
