using System;

namespace EventsExpress.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public CategoryGroupViewModel CategoryGroup { get; set; }

        public int CountOfUser { get; set; }

        public int CountOfEvents { get; set; }
    }
}
