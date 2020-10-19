using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            _mapper = mapper;
        }

        public Category Get(Guid id) => db.CategoryRepository.Get(id);

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = _mapper.Map<IEnumerable<CategoryDTO>>(db.CategoryRepository.Get().AsEnumerable());
            foreach (var cat in categories)
            {
                cat.CountOfUser = db.UserRepository.Get("Categories").Where(x => x.Categories.Any(c => c.Category.Name == cat.Name)).Count();
                cat.CountOfEvents = db.EventRepository.Get("Categories").Where(x => x.Categories.Any(c => c.Category.Name == cat.Name)).Count();
            }

            return categories;
        }

        public async Task<OperationResult> Create(string title)
        {
            if (db.CategoryRepository.Get().Any(c => c.Name == title))
            {
                return new OperationResult(false, "The same category is already exist in database", string.Empty);
            }

            db.CategoryRepository.Insert(new Category { Name = title });
            await db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Edit(CategoryDTO category)
        {
            var oldCategory = db.CategoryRepository.Get(category.Id);
            if (oldCategory == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            if (db.CategoryRepository.Get().Any(c => c.Name == category.Name))
            {
                return new OperationResult(false, "The same category is already exist in database", string.Empty);
            }

            oldCategory.Name = category.Name;
            await db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            var category = db.CategoryRepository.Get(id);
            if (category == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            var result = db.CategoryRepository.Delete(category);
            if (result.Id != id)
            {
                return new OperationResult(false, string.Empty, string.Empty);
            }

            await db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
