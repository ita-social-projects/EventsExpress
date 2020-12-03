using System.Linq;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class CategoryViewModelEnumerationValidation : AbstractValidator<EditUserCategoriesViewModel>
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewModelEnumerationValidation(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            RuleFor(x => x.Categories)
                .Must(x => _categoryService.ExistsAll(x.Select(x => x.Id)))
                .WithMessage("Invalid category id");
        }
    }
}
