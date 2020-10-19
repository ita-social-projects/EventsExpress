using System;
using System.Collections.Generic;
using EventsExpress.Db.Entities;

namespace EventsExpress.Db.IRepo
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByTitle(string title);

        List<Category> EventCategories(Guid id);
    }
}
