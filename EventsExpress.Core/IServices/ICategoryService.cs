using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();

        Category GetById(Guid id);

        Task Create(string title);

        Task Edit(CategoryDto category);

        Task Delete(Guid id);

        bool Exists(Guid id);

        bool ExistsByName(string categoryName);

        bool ExistsAll(IEnumerable<Guid> ids);
    }
}
