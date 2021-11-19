namespace EventsExpress.Validation
{
    using EventsExpress.ViewModels;
    using FluentValidation;

    public class UserMoreInfoCreateViewModelValidator : AbstractValidator<UserMoreInfoCreateViewModel>
    {
        public UserMoreInfoCreateViewModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Name is required");
        }
    }
}
