using System.Linq;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EditUserNameViewModelValidator : AbstractValidator<EditUserNameViewModel>
    {
        public EditUserNameViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).Must(x => !char.IsLetter(x.First())).WithMessage("Username must begin with a letter");
            RuleFor(x => x.Name).Must(x => !char.IsLetter(x.Last())).WithMessage("Username cannot contain special characters at the end");
            RuleFor(x => x.Name).Must(x => x.Contains(' ')).WithMessage("Spaces are not allowed");
            RuleFor(x => x.Name).Must(x => x.Length >= 3 && x.Length <= 25).WithMessage("Username should be between 3 and 25 characters in length");
        }
    }
}
