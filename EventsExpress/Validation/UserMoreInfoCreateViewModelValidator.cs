using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation;

public class UserMoreInfoCreateViewModelValidator : AbstractValidator<UserMoreInfoCreateViewModel>
{
    public UserMoreInfoCreateViewModelValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is missing");
    }
}
