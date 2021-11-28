using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class CategoryEditViewModelValidator : AbstractValidator<CategoryEditViewModel>
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryGroupService _categoryGroupService;
        private readonly IMapper _mapper;

        public CategoryEditViewModelValidator(
            ICategoryService categoryService,
            ICategoryGroupService categoryGroupService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _categoryGroupService = categoryGroupService;
            _mapper = mapper;

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name can not be null!");

            RuleFor(x => x.Name)
                .Length(2, 20)
                .WithMessage("Name length exceeded the recommended length of 20 characters!");

            RuleFor(x => x)
                .Must(x => !_categoryService.ExistsByName(x.Name) || !_categoryService.IsDuplicate(_mapper.Map<CategoryDto>(x)))
                .WithMessage("The same category already exists!");

            RuleFor(x => x.Id)
                .Must(id => _categoryService.Exists(id))
                .WithMessage("The category not exists!");

            RuleFor(x => x.CategoryGroupId)
                .Must(categoryGroupId => _categoryGroupService.Exists(categoryGroupId))
                .WithMessage("The category group does not exist!");
        }
    }
}
