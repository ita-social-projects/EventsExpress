using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class CategoryCreateViewModelValidator : AbstractValidator<CategoryCreateViewModel>
    {
        private readonly ICategoryService _categoryService;

        public CategoryCreateViewModelValidator(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name can not be null!");

            RuleFor(x => x.Name)
                .Length(2, 20)
                .WithMessage("Name length exceeded the recommended length of 20 characters!");

            RuleFor(x => x.Name)
                .Must(name => !_categoryService.ExistsByName(name))
                .WithMessage("The same category already exists!");
        }
    }
}
