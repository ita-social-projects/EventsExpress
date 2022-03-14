using System;
using System.Text.RegularExpressions;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation;

public class RegisterCompleteViewModelValidator : AbstractValidator<RegisterCompleteViewModel>
{
    public RegisterCompleteViewModelValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Birthday)
            .NotEmpty()
            .WithMessage("Birthday date is required")
            .InclusiveBetween(DateTime.Now.AddYears(-115), DateTime.Now.AddYears(-14))
            .WithMessage("Birthday date is not correct");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required")
            .EmailAddress()
            .WithMessage("Email address is not correct");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .WithMessage("Gender is required")
            .IsInEnum()
            .WithMessage("Gender is not correct");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Must(BeAValidPhoneNumber)
            .WithMessage("Phone number is not correct");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MinimumLength(3)
            .WithMessage("First name is too short")
            .MaximumLength(50)
            .WithMessage("First name is too long");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MinimumLength(3)
            .WithMessage("Last name is too short")
            .MaximumLength(50)
            .WithMessage("Last name is too long");
    }

    private static bool BeAValidPhoneNumber(string phone)
    {
        string phoneDigits = Regex.Replace(phone, @"\D", string.Empty);
        return Regex.IsMatch(phoneDigits, @"\d{8,15}");
    }
}
