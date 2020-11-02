using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Db.Entities
{
    public class ManageableEvent : BaseEntity
    {
        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
