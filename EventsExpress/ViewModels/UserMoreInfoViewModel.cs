namespace EventsExpress.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;

    [ExcludeFromCodeCoverage]
    public class UserMoreInfoViewModel
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public ParentStatus ParentStatus { get; set; }

        public IEnumerable<EventTypes> EventTypes { get; set; }

        public RelationShipStatus RelationShipStatus { get; set; }

        public TheTypeOfLeisure TheTypeOfLeisure { get; set; }

        public IEnumerable<InterestReasons> ReasonsForUsingTheSite { get; set; }

        public string AditionalInfoAboutUser { get; set; }
    }
}
