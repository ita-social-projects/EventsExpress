using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsExpress.Core.IServices
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
    }
}
