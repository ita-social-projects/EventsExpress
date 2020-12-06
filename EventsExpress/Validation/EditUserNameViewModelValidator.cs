using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EditUserNameViewModelValidator : AbstractValidator<EditUserNameViewModel>
    {
        public EditUserNameViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
