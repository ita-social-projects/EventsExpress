using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class ContactAdminViewModelValidator : AbstractValidator<ContactAdminViewModel>
    {
        public ContactAdminViewModelValidator()
        {
            RuleFor(x => x.Title).MaximumLength(30).WithMessage("Title length exceeded the recommended length of 30 character!");
        }
    }
}
