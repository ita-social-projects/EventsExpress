using System;

namespace EventsExpress.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int CountOfUser { get; set; }

        public int CountOfEvents { get; set; }
    }
}
