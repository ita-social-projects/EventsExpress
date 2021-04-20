using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EditNotificationTemplateViewModelValidator : AbstractValidator<EditNotificationTemplateViewModel>
    {
        public EditNotificationTemplateViewModelValidator()
        {
            RuleFor(e => e.Subject).NotEmpty()
                .MinimumLength(10).WithMessage("The subject must contain a minimum of 10 characters.");
            RuleFor(e => e.Message).NotEmpty()
                .MinimumLength(15).WithMessage("The message must contain a minimum of 15 characters");
        }
    }
}
