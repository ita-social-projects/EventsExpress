using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class UserMoreInfo : BaseEntity
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public ParentStatus ParentStatus { get; set; }

        public UserMoreInfoEventType EventType { get; set; }

        public RelationShipStatus RelationShipStatus { get; set; }

        public TheTypeOfLeisure TheTypeOfLeisure { get; set; }

        public UserMoreInfoReasonsForUsingTheSite ReasonsForUsingTheSite { get; set; }

        public string AditionalInfoAboutUser { get; set; }
    }
}
