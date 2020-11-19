using System;
using System.Collections.Generic;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;

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

        public Guid OwnerId { get; set; }

        public User Owner { get; set; }

        public Guid CityId { get; set; }

        public City City { get; set; }

        public IEnumerable<CategoryDTO> Categories { get; set; }

        public IEnumerable<UserEvent> Visitors { get; set; }
    }
}
