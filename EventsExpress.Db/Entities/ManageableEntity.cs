using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class ManageableEntity : BaseEntity
    {
        public Guid CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        public Guid ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
