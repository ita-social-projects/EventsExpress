using System;

namespace EventsExpress.Core.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        // public Guid CategoryGroupId { get; set; }
        public CategoryGroupDto CategoryGroup { get; set; }

        public int CountOfUser { get; set; }

        public int CountOfEvents { get; set; }
    }
}
