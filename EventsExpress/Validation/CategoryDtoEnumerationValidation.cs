using System.Linq;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class CategoryDtoEnumerationValidation : AbstractValidator<EditUserCategoriesDto>
    {
        private readonly ICategoryService _categoryService;

        public CategoryDtoEnumerationValidation(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            RuleFor(x => x.Categories)
                .Must(x => _categoryService.ExistsAll(x.Select(x => x.Id)))
                .WithMessage("Invalid category id");
        }
    }
}
