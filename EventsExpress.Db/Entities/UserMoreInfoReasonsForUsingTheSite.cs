namespace EventsExpress.Db.Entities
{
    using System;
    using EventsExpress.Db.Enums;

    public class UserMoreInfoReasonsForUsingTheSite : BaseEntity
    {
        public Guid UserMoreInfoId { get; set; }

        public virtual UserMoreInfo UserMoreInfo { get; set; }

        public ReasonsForUsingTheSite ReasonsForUsingTheSite { get; set; }
    }
}
