using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.DTOs
{
   public class EventDTO
    {
        public bool IsBlocked { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public List<string> Categories { get; set; }
        public IFormFile Photo { get; set; }

        public Guid UserId { get; set; }
        public City City { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
