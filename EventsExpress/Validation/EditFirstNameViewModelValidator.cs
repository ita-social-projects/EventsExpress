using System.Linq;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation;

public class EditFirstNameViewModelValidator : AbstractValidator<EditFirstNameViewModel>
{
    public EditFirstNameViewModelValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().OverridePropertyName("firstName").WithMessage("FirstName is required");
        RuleFor(x => x.FirstName).Must(x => char.IsLetter(x.First())).OverridePropertyName("firstName").WithMessage("FirstName must begin with a letter");
        RuleFor(x => x.FirstName).Must(x => char.IsLetter(x.Last())).OverridePropertyName("firstName").WithMessage("FirstName cannot contain special characters at the end");
        RuleFor(x => x.FirstName).Must(x => !x.Contains(' ')).OverridePropertyName("firstName").WithMessage("Spaces are not allowed");
        RuleFor(x => x.FirstName).Must(x => x.Length >= 3 && x.Length <= 25).OverridePropertyName("firstName").WithMessage("FirstName should be between 3 and 25 characters in length");
    }
}
