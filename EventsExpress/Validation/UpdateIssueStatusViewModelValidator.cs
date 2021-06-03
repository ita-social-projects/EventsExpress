using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class UpdateIssueStatusViewModelValidator : AbstractValidator<UpdateIssueStatusViewModel>
    {
        public UpdateIssueStatusViewModelValidator()
        {
            RuleFor(x => x.ResolutionDetails).NotEmpty().WithMessage("Resolution details is required")
                                  .Must(r => r.Length > 5 && r.Length < 1000)
                                  .WithMessage("Resolution details should be between 5 and 1000 characters");
        }
    }
}
