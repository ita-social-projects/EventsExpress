using System;
using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities
{
    public class ManageableEntity : BaseEntity
    {
        [Track]
        public Guid? CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        [Track]
        public Guid? ModifiedBy { get; set; }

        [Track]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
