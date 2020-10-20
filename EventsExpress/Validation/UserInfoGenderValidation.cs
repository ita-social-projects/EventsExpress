using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class UserInfoGenderValidation : AbstractValidator<UserInfo>
    {
        public UserInfoGenderValidation()
        {
            RuleFor(x => x.Gender)
                .InclusiveBetween((byte)0, (byte)2)
                .WithMessage("InvalidGender");
        }
    }
}
