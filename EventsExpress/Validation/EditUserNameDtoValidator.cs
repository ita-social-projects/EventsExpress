using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EditUserNameDtoValidator : AbstractValidator<EditUserNameDto>
    {
        public EditUserNameDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
