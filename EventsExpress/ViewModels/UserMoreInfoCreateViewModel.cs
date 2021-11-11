namespace EventsExpress.ViewModels
{
    using System;
    using EventsExpress.Db.Enums;

    public class UserMoreInfoCreateViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ParentStatus ParentStatus { get; set; }

        public RelationShipStatus RelationShipStatus { get; set; }

        public TheTypeOfLeisure TheTypeOfLeisure { get; set; }

        public string AditionalInfoAboutUser { get; set; }
    }
}
