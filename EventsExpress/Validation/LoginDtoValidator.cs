using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Address  is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email Address is not correct");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
