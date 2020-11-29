using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EditUserGenderDtoValidator : AbstractValidator<EditUserGenderDto>
    {
        public EditUserGenderDtoValidator()
        {
            RuleFor(x => x.Gender)
                .InclusiveBetween((short)0, (short)2)
                .WithMessage("InvalidGender");
        }
    }
}
