using System;
using System.Collections.Generic;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.DTOs
{
    public class ChildEventDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public Point Point { get; set; }

        public LocationType Type { get; set; }

        public Uri OnlineMeeting { get; set; }

        public EventStatus EventStatus { get; set; }

        public bool? IsMultiEvent { get; set; }
    }
}
