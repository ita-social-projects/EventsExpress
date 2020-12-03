using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EditUserGenderViewModelValidator : AbstractValidator<EditUserGenderViewModel>
    {
        public EditUserGenderViewModelValidator()
        {
            RuleFor(x => x.Gender)
                .InclusiveBetween((short)0, (short)2)
                .WithMessage("InvalidGender");
        }
    }
}
