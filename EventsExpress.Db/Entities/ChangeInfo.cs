using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class ChangeInfo : BaseEntity
    {
        public string EntityName { get; set; }

        public Guid EntityId { get; set; }

        public Guid UserId { get; set; }

        public string PropertyChangesText { get; set; }

        public ChangesType ChangesType { get; set; }
    }
}
