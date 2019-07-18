using EventsExpress.Db.IRepo;
using EventsExpress.Db.Repo;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventsExpress.Db.EF;

namespace EventsExpress.Db.Repo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext db): base(db)
        {

        }
        public Category GetByTitle(string title)
        {
            return Database.Categories.FirstOrDefault(x => x.Name == title);
        }
        public List<Category> EventCategories(Guid id)
        {
            List<Category> categories = new List<Category>();
            categories = Database.Categories.Where(c => c.Events
                                .Any(e => e.EventId == id))
                                .ToList();
            return categories;
        }
    }
}
