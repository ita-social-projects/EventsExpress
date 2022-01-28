namespace EventsExpress.Validation
{
    using EventsExpress.ViewModels;
    using FluentValidation;

    public class UserMoreInfoCreateViewModelValidator : AbstractValidator<UserMoreInfoCreateViewModel>
    {
        public UserMoreInfoCreateViewModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is not correct");
        }
    }
}
