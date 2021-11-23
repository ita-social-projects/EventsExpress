namespace EventsExpress.Db.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using EventsExpress.Db.Enums;

    [ExcludeFromCodeCoverage]
    public class UserMoreInfoReasonsForUsingTheSite : BaseEntity
    {
        public Guid UserMoreInfoId { get; set; }

        public virtual UserMoreInfo UserMoreInfo { get; set; }

        public ReasonsForUsingTheSite ReasonsForUsingTheSite { get; set; }
    }
}
