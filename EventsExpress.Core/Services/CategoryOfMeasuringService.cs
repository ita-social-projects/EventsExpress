using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class CategoryOfMeasuringService : BaseService<CategoryOfMeasuring>, ICategoryOfMeasuringService
    {
        public CategoryOfMeasuringService(
            AppDbContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public IEnumerable<CategoryOfMeasuringDto> GetAllCategories()
        {
            return Mapper.Map<IEnumerable<CategoryOfMeasuringDto>>(
                Context.CategoriesOfMeasurings
                .OrderBy(category => category.CategoryName));
        }
    }
}
