using System;
using System.Collections.Generic;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.DTOs
{
    public class EventDTO
    {
        public Guid Id { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsReccurent { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int MaxParticipants { get; set; }

        public int Frequency { get; set; }

        public Periodicity Periodicity { get; set; }

        public IFormFile Photo { get; set; }

        public string PhotoUrl { get; set; }

        public Guid PhotoId { get; set; }

        public Photo PhotoBytes { get; set; }

        public bool IsPublic { get; set; }

        public Point Point { get; set; }

        public IEnumerable<CategoryDTO> Categories { get; set; }

        public IEnumerable<UserEvent> Visitors { get; set; }

        public IEnumerable<InventoryDTO> Inventories { get; set; }

        public IEnumerable<Guid> OwnerIds { get; set; }

        public IEnumerable<User> Owners { get; set; }
    }
}
