using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Address  is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email Address is not correct");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
