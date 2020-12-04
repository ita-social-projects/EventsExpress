using System;
using System.Collections.Generic;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class EventScheduleViewModel
    {
        public Guid Id { get; set; }

        public int Frequency { get; set; }

        public Periodicity Periodicity { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public bool IsActive { get; set; }

        public string Title { get; set; }

        public string PhotoUrl { get; set; }

        public IEnumerable<UserPreviewViewModel> Owners { get; set; }

        public Guid EventId { get; set; }
    }
}
