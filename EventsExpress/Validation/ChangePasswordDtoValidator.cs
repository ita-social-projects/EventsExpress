using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.NewPassword).NotEqual(x => x.OldPassword).WithMessage("New Password can't be the same as Old Password!");
        }
    }
}
