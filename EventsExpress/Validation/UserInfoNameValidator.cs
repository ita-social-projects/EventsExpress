using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class UserInfoNameValidator : AbstractValidator<UserInfo>
    {
        public UserInfoNameValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
