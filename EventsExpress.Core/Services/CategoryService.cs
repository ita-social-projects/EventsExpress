using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
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
            return Context.Categories.Find(id);
        }

        public IEnumerable<CategoryDto> GetAllCategories(Guid? groupId)
        {
            var categories = Context.Categories
                                .Include(c => c.Users)
                                .Include(c => c.Events)
                                .Where(c => !groupId.HasValue || c.CategoryGroupId == groupId)
                                .ProjectTo<CategoryDto>(Mapper.ConfigurationProvider)
                                .OrderBy(category => category.Name);

            return categories;
        }

        public async Task Create(CategoryDto category)
        {
            var newCategory = Mapper.Map<CategoryDto, Category>(category);
            Insert(newCategory);
            await Context.SaveChangesAsync();
        }

        public async Task Edit(CategoryDto category)
        {
            var oldCategory = Context.Categories.Find(category.Id);
            if (oldCategory == null)
            {
                throw new EventsExpressException("Not found");
            }

            oldCategory.Name = category.Name;
            oldCategory.CategoryGroupId = category.CategoryGroup.Id;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var category = Context.Categories.Find(id);
            if (category == null)
            {
                throw new EventsExpressException("Not found");
            }

            var result = Delete(category);
            if (result.Id != id)
            {
                throw new EventsExpressException(string.Empty);
            }

            await Context.SaveChangesAsync();
        }

        public bool Exists(Guid id) =>
            Context.Categories.Any(x => x.Id == id);

        public bool ExistsByName(string categoryName) =>
            Context.Categories.Any(x => x.Name == categoryName);

        public bool ExistsAll(IEnumerable<Guid> ids) =>
            Context.Categories.Count(x => ids.Contains(x.Id)) == ids.Count();

        public bool IsDuplicate(CategoryDto category) =>
            Context.Categories.Any(x => x.Id == category.Id && x.CategoryGroup.Id == category.CategoryGroup.Id);
    }
}
