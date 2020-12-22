using System;
using EventsExpress.ViewModels.Base;
using EventsExpress.Db.Enums;

namespace EventsExpress.ViewModels
{
    public class EventPreviewViewModel : EventViewModelBase
    {
        public Guid Id { get; set; }

        public string PhotoUrl { get; set; }

        public bool IsBlocked { get; set; }

        public int CountVisitor { get; set; }

        public bool IsPublic { get; set; }

        public EventStatus EventStatus { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
