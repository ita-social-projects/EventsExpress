using System;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.DTOs
{
    public class DeveloperDTO
    {
        public string Name { get; set; }

        public Guid TeamId { get; set; }

        public IFormFile Photo { get; set; }

        public string Description { get; set; }
    }
}
