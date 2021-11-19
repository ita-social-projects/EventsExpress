using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.Entities
{
    public class UserMoreInfo : BaseEntity
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public ParentStatus ParentStatus { get; set; }

        public IEnumerable<UserMoreInfoEventType> EventTypes { get; set; }

        public RelationShipStatus RelationShipStatus { get; set; }

        public TheTypeOfLeisure TheTypeOfLeisure { get; set; }

        public IEnumerable<UserMoreInfoReasonsForUsingTheSite> ReasonsForUsingTheSite { get; set; }

        public string AditionalInfoAboutUser { get; set; }
    }
}
