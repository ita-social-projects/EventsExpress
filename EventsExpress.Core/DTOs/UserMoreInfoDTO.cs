namespace EventsExpress.Core.DTOs
{
    using System;
    using System.Collections.Generic;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;

    public class UserMoreInfoDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ParentStatus ParentStatus { get; set; }

        public IEnumerable<EventType> EventTypes { get; set; }

        public RelationShipStatus RelationShipStatus { get; set; }

        public TheTypeOfLeisure TheTypeOfLeisure { get; set; }

        public IEnumerable<ReasonsForUsingTheSite> ReasonsForUsingTheSite { get; set; }

        public string AditionalInfoAboutUser { get; set; }
    }
}
