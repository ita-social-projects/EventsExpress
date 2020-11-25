using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.DTOs
{
    public class TeamDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<IFormFile> Photos { get; set; }
    }
}
