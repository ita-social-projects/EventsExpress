using System.Collections.Generic;

namespace EventsExpress.Db.Entities
{
    public class CategoryGroup : BaseEntity
    {
        public string Title { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}
