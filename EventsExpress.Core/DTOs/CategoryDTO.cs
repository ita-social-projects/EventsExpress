using System;

namespace EventsExpress.Core.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int CountOfUser { get; set; }

        public int CountOfEvents { get; set; }
    }
}
