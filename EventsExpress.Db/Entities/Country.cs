using System.Collections.Generic;

namespace EventsExpress.Db.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public virtual IEnumerable<City> Cities { get; set; }
    }
}
