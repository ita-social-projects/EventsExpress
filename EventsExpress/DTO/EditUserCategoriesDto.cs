using System.Collections.Generic;
using EventsExpress.Core.DTOs;

namespace EventsExpress.DTO
{
    public class EditUserCategoriesDto
    {
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
