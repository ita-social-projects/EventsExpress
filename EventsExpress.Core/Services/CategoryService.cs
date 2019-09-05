using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class CategoryService : ICategoryService
    {
        public IUnitOfWork Db { get; set; }
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            Db = uow;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
           return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(Db.CategoryRepository.Get().AsEnumerable());
        } 

        public Category Get(Guid id) => Db.CategoryRepository.Get(id);

        public async  Task<OperationResult> Create(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return new OperationResult(false, "Incorrect category name!", "");
            }

            if (Db.CategoryRepository.Get().Any(c => c.Name == title))
            {
                return new OperationResult(false, "The same category is already exist in database", "");
            }

            Db.CategoryRepository.Insert(new Category() {Name=title });

            await Db.SaveAsync();

            return new OperationResult(true, "", "");
        }

        public async Task<OperationResult> Edit(CategoryDTO category)
        {
            if (category.Id == null)
            {
                return new OperationResult(false, "Id field is '0'", "");
            }

            Category oldCategory = Db.CategoryRepository.Get(category.Id);
            if (oldCategory == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            if (Db.CategoryRepository.Get().Any(c => c.Name == category.Name))
            {
                return new OperationResult(false, "The same category is already exist in database", "");
            }

            oldCategory.Name = category.Name;

            await Db.SaveAsync();

            return new OperationResult(true, "", "");
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new OperationResult(false, "Id field is null", "");
            }
            Category category = Db.CategoryRepository.Get(id);
            if (category == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            var result = Db.CategoryRepository.Delete(category);
            await Db.SaveAsync();
            return new OperationResult(true, "", "");
        }

    }
}
