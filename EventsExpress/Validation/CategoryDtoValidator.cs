using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name can not be null!");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("Name length exceeded the recommended length of 20 characters!");
        }
    }
}
