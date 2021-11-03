namespace EventsExpress.Db.Entities
{
    using System;
    using System.Collections.Generic;
    using EventsExpress.Db.Enums;

    public class UserMoreInfoEventType : BaseEntity
    {
        public Guid UserMoreInfoId { get; set; }

        public virtual UserMoreInfo UserMoreInfo { get; set; }

        public EventType EventType { get; set; }
    }
}
