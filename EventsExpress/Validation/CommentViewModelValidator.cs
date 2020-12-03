using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class CommentViewModelValidator : AbstractValidator<CommentViewModel>
    {
        public CommentViewModelValidator()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required!");
            RuleFor(x => x.UserId).NotNull().WithMessage("Error userId is null!");
            RuleFor(x => x.EventId).NotNull().WithMessage("Error eventId is null!");
        }
    }
}
