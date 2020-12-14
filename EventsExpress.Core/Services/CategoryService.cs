using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Category GetById(Guid id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryDTO>>(
                _context.Categories.Include(c => c.Users).Include(c => c.Events).Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    CountOfEvents = x.Events.Count(),
                    CountOfUser = x.Users.Count(),
                }));

            return categories;
        }

        public async Task Create(string title)
        {
            if (!ExistsByName(title))
            {
                Insert(new Category { Name = title });
                await _context.SaveChangesAsync();
            }
        }

        public async Task Edit(CategoryDTO category)
        {
            var oldCategory = _context.Categories.Find(category.Id);
            if (oldCategory == null)
            {
                throw new EventsExpressException("Not found");
            }

            if (ExistsByName(category.Name))
            {
                throw new EventsExpressException("The same category is already exist in database");
            }

            oldCategory.Name = category.Name;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new EventsExpressException("Not found");
            }

            var result = Delete(category);
            if (result.Id != id)
            {
                throw new EventsExpressException(string.Empty);
            }

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id) =>
            _context.Categories.Any(x => x.Id == id);

        public bool ExistsByName(string categoryName) =>
            _context.Categories.Any(x => x.Name == categoryName);

        public bool ExistsAll(IEnumerable<Guid> ids) =>
            _context.Categories.Count(x => ids.Contains(x.Id)) == ids.Count();
    }
}
