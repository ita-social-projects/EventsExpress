using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Category GetById(Guid id)
        {
            return Get(id);
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryDTO>>(
                Get().ToList());

            foreach (var cat in categories)
            {
                cat.CountOfUser = _context.Users.Include("Categories")
                    .Where(x => x.Categories.Any(c => c.Category.Name == cat.Name))
                    .Count();

                cat.CountOfEvents = _context.Events.Include("Categories")
                    .Where(x => x.Categories.Any(c => c.Category.Name == cat.Name))
                    .Count();
            }

            return categories;
        }

        public async Task<OperationResult> Create(string title)
        {
            if (Get().Any(c => c.Name == title))
            {
                return new OperationResult(false, "The same category is already exist in database", string.Empty);
            }

            Insert(new Category { Name = title });
            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Edit(CategoryDTO category)
        {
            var oldCategory = Get(category.Id);
            if (oldCategory == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            if (Get().Any(c => c.Name == category.Name))
            {
                return new OperationResult(false, "The same category is already exist in database", string.Empty);
            }

            oldCategory.Name = category.Name;
            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            var category = Get(id);
            if (category == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            var result = Delete(category);
            if (result.Id != id)
            {
                return new OperationResult(false, string.Empty, string.Empty);
            }

            await _context.SaveChangesAsync();
            return new OperationResult(true);
        }

        public bool Exists(Guid id) =>
            Get().Count(x => x.Id == id) > 0;

        public bool ExistsAll(IEnumerable<Guid> ids) =>
            Get().Count(x => ids.Contains(x.Id)) == ids.Count();
    }
}
