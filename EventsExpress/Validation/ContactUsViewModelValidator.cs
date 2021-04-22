using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class ContactUsViewModelValidator : AbstractValidator<ContactUsViewModel>
    {
        public ContactUsViewModelValidator()
        {
           // RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
           // RuleFor(x => x.Title).MaximumLength(30).WithMessage("Title length exceeded the recommended length of 30 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Address  is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email Address is not correct");
        }
    }
}
