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
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _db = uow;
            _mapper = mapper;
        }

        public Category Get(Guid id) => _db.CategoryRepository.Get(id);

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryDTO>>(
                _db.CategoryRepository.Get()
                .AsNoTracking()
                .ToList());

            foreach (var cat in categories)
            {
                cat.CountOfUser = _db.UserRepository.Get("Categories")
                    .AsNoTracking()
                    .Where(x => x.Categories.Any(c => c.Category.Name == cat.Name))
                    .Count();

                cat.CountOfEvents = _db.EventRepository.Get("Categories")
                    .AsNoTracking()
                    .Where(x => x.Categories.Any(c => c.Category.Name == cat.Name))
                    .Count();
            }

            return categories;
        }

        public async Task<OperationResult> Create(string title)
        {
            if (_db.CategoryRepository.Get().Any(c => c.Name == title))
            {
                return new OperationResult(false, "The same category is already exist in database", string.Empty);
            }

            _db.CategoryRepository.Insert(new Category { Name = title });
            await _db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Edit(CategoryDTO category)
        {
            var oldCategory = _db.CategoryRepository.Get(category.Id);
            if (oldCategory == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            if (_db.CategoryRepository.Get().Any(c => c.Name == category.Name))
            {
                return new OperationResult(false, "The same category is already exist in database", string.Empty);
            }

            oldCategory.Name = category.Name;
            await _db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            var category = _db.CategoryRepository.Get(id);
            if (category == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            var result = _db.CategoryRepository.Delete(category);
            if (result.Id != id)
            {
                return new OperationResult(false, string.Empty, string.Empty);
            }

            await _db.SaveAsync();
            return new OperationResult(true);
        }

        public bool Exists(Guid id) => _db.CategoryRepository.Exists(id);

        public bool ExistsAll(IEnumerable<Guid> ids) => _db.CategoryRepository.ExistsAll(ids);
    }
}
