using System;
using System.Collections.Generic;

namespace EventsExpress.DTO
{
    public class TeamDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<DeveloperDto> Developers { get; set; }

        public ICollection<string> Photos { get; set; }
    }
}
