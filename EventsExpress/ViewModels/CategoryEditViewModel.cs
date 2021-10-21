using System;

namespace EventsExpress.ViewModels
{
    public class CategoryEditViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CategoryGroupId { get; set; }
    }
}
