using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();

        Category GetById(Guid id);

        Task Create(string title);

        Task Edit(CategoryDTO category);

        Task Delete(Guid id);

        bool Exists(Guid id);

        bool ExistsAll(IEnumerable<Guid> ids);
    }
}
