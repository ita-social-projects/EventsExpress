using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class ContactUsViewModelValidator : AbstractValidator<ContactUsViewModel>
    {
        public ContactUsViewModelValidator()
        {
            RuleFor(x => x.Title).MaximumLength(30).WithMessage("Title length exceeded the recommended length of 30 character!");
        }
    }
}
