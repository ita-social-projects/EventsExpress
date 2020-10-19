using System;
using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class UserInfoAgeValidator : AbstractValidator<UserInfo>
    {
        public UserInfoAgeValidator()
        {
            RuleFor(x => x.Birthday).Must(BeLess115).WithMessage("Must be less then 115 years old");
            RuleFor(x => x.Birthday).Must(BeOver18).WithMessage("Must be over then 14 years old");
        }

        protected bool BeOver18(DateTime date) => date <= DateTime.Today.AddYears(-14);

        protected bool BeLess115(DateTime date) => date >= DateTime.Today.AddYears(-115);
    }
}
