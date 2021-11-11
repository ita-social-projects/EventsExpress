namespace EventsExpress.Core.DTOs
{
    using System;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;

    public class UserMoreInfoDTO
    {
        public Guid Id { get; set; }

        public virtual User User { get; set; }

        public ParentStatus ParentStatus { get; set; }

        public UserMoreInfoEventType EventType { get; set; }

        public RelationShipStatus RelationShipStatus { get; set; }

        public TheTypeOfLeisure TheTypeOfLeisure { get; set; }

        public UserMoreInfoReasonsForUsingTheSite ReasonsForUsingTheSite { get; set; }

        public string AditionalInfoAboutUser { get; set; }
    }
}
