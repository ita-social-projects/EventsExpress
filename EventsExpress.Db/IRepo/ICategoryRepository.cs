using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsExpress.Db.IRepo
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByTitle(string title);
        List<Category> EventCategories(Guid id);
    }
}
