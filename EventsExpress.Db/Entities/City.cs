using System;

namespace EventsExpress.Db.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public Guid CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
