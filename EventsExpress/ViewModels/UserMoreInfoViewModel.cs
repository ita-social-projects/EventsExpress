namespace EventsExpress.ViewModels
{
    using System;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;

    public class UserMoreInfoViewModel
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public ParentStatus ParentStatus { get; set; }

        public UserMoreInfoEventType EventType { get; set; }

        public RelationShipStatus RelationShipStatus { get; set; }

        public TheTypeOfLeisure TheTypeOfLeisure { get; set; }

        public UserMoreInfoReasonsForUsingTheSite ReasonsForUsingTheSite { get; set; }

        public string AditionalInfoAboutUser { get; set; }
    }
}
