using System.Collections.Generic;
using EventsExpress.Core.DTOs;

namespace EventsExpress.ViewModels
{
    public class EditUserCategoriesViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
