using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Db.Repo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext db)
            : base(db)
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

        public bool Exists(Guid id) =>
            Database.Categories.Count(x => x.Id == id) > 0;

        public bool ExistsAll(IEnumerable<Guid> ids) =>
            Database.Categories.Count(x => ids.Contains(x.Id)) == ids.Count();
    }
}
