using System;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.DTOs
{
    public class TrackDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public User User { get; set; }

        public string EntityKeys { get; set; }

        public string PropertyChangesText { get; set; }

        public ChangesType ChangesType { get; set; }

        public DateTime Time { get; set; }
    }
}
