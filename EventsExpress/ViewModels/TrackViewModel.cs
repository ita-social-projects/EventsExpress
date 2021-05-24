using System;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class TrackViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string EntityName { get; set; }

        public string EntityKeys { get; set; }

        public string PropertyChangesText { get; set; }

        public ChangesType ChangesType { get; set; }

        public DateTime Time { get; set; }
    }
}
