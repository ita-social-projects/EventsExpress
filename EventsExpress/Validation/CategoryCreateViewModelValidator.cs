using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class CategoryCreateViewModelValidator : AbstractValidator<CategoryCreateViewModel>
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryGroupService _categoryGroupService;

        public CategoryCreateViewModelValidator(
            ICategoryService categoryService,
            ICategoryGroupService categoryGroupService)
        {
            _categoryService = categoryService;
            _categoryGroupService = categoryGroupService;

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name can not be null!");

            RuleFor(x => x.Name)
                .Length(2, 20)
                .WithMessage("Name length exceeded the recommended length of 20 characters!");

            RuleFor(x => x.Name)
                .Must(name => !_categoryService.ExistsByName(name))
                .WithMessage("The same category already exists!");

            RuleFor(x => x.CategoryGroup)
                .NotNull()
                .WithMessage("Category group must have been chosen!");

            RuleFor(x => x.CategoryGroup)
                .Must(group => _categoryGroupService.Exists(group.Id))
                .WithMessage("The category group does not exist!");
        }
    }
}
