using EventsExpress.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Validation
{
    public class UserInfoValidator : AbstractValidator<UserInfo>
    {
        public UserInfoValidator()
        {
            RuleSet("Birthday", () => {
                Include(new UserInfoAgeValidator());
            });
            
            //Include(new UserInfoNameValidator());
            //RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Id can not be null!");
           
        }
    }
}
