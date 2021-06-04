using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface ICategoryOfMeasuringService
    {
        IEnumerable<CategoryOfMeasuringDto> GetAllCategories();

        CategoryOfMeasuringDto GetCategoryOfMeasuringById(Guid categoryId);
    }
}
