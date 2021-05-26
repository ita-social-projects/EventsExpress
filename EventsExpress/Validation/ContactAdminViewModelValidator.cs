using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class ContactAdminViewModelValidator : AbstractValidator<ContactAdminViewModel>
    {
        public ContactAdminViewModelValidator()
        {
            RuleFor(x => x.Title).MaximumLength(30).WithMessage("Title length exceeded the recommended length of 30 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Address  is required")
                                 .EmailAddress().WithMessage("Email Address is not correct");
        }
    }
}
