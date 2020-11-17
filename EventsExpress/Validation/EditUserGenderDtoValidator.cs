using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EditUserGenderDtoValidator : AbstractValidator<EditUserGenderDto>
    {
        public EditUserGenderDtoValidator()
        {
            RuleFor(x => x.Gender)
                .InclusiveBetween((byte)0, (byte)2)
                .WithMessage("InvalidGender");
        }
    }
}
