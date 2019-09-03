using EventsExpress.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Validation
{
    public class UserInfoGenderValidation:AbstractValidator<UserInfo>
    {
        public UserInfoGenderValidation()
        {
            RuleFor(x => x.Gender)
                .InclusiveBetween((byte)0, (byte)2)
                .WithMessage("InvalidGender");
        }
    }
}
