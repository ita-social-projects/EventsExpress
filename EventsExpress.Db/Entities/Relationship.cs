using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class Relationship : BaseEntity
    {
        public Guid UserFromId { get; set; }

        public virtual User UserFrom { get; set; }

        public Guid UserToId { get; set; }

        public virtual User UserTo { get; set; }

        public Attitude Attitude { get; set; }
    }
}
