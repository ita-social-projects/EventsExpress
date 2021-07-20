using System;
using System.Text.RegularExpressions;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class RegisterCompleteViewModelValidator : AbstractValidator<RegisterCompleteViewModel>
    {
        public RegisterCompleteViewModelValidator()
        {
            RuleFor(x => x.Birthday).NotEmpty().WithMessage("Birthday date is required");
            RuleFor(x => x.Birthday).InclusiveBetween(DateTime.Now.AddYears(-115), DateTime.Now.AddYears(-15)).WithMessage("Birthday date is not correct");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email address is not correct");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required");
            RuleFor(x => x.Gender).IsInEnum().WithMessage("Gender is not correct");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phonenumber is required");
            RuleFor(x => x.Phone).Must(Phonenumber).WithMessage("Phonenumber is not correct");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Username).MinimumLength(3).WithMessage("Username is too short");
            RuleFor(x => x.Username).MaximumLength(50).WithMessage("Username is too long");
        }

        private bool Phonenumber(string phone)
        {
            phone = Regex.Replace(phone, @"\D", string.Empty);

            return Regex.IsMatch(phone, @"\d{8,15}");
        }
    }
}
