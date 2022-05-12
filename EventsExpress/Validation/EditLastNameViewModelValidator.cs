using System.Linq;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation;

public class EditLastNameViewModelValidator : AbstractValidator<EditLastNameViewModel>
{
    public EditLastNameViewModelValidator()
    {
        RuleFor(x => x.LastName).Must(x => char.IsLetter(x.First())).OverridePropertyName("lastName").WithMessage("Last name must begin with a letter");
        RuleFor(x => x.LastName).Must(x => char.IsLetter(x.Last())).OverridePropertyName("lastName").WithMessage("Last name cannot contain special characters at the end");
        RuleFor(x => x.LastName).Must(x => !x.Contains(' ')).OverridePropertyName("lastName").WithMessage("Spaces are not allowed");
        RuleFor(x => x.LastName).Must(x => x.Length >= 3 && x.Length <= 25).OverridePropertyName("lastName").WithMessage("Last name should be between 3 and 25 characters in length");
    }
}
