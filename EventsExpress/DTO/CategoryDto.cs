using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
