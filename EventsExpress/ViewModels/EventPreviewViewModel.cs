using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace EventsExpress.ViewModels
{
    public class EventPreviewViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int MaxParticipants { get; set; }

        public string PhotoUrl { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsBlocked { get; set; }

        public int CountVisitor { get; set; }

        public bool IsPublic { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<UserPreviewViewModel> Owners { get; set; }
    }
}
