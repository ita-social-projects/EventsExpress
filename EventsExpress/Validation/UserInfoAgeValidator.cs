using EventsExpress.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Validation
{
    public class UserInfoAgeValidator:AbstractValidator<UserInfo>
    {
        public UserInfoAgeValidator()
        {
            RuleFor(x => x.Birthday).Must(BeOver18).Must(BeLess115);
            
        }

        protected bool BeOver18(DateTime date) => (date >= DateTime.Today.AddYears(-18));
        
        protected bool BeLess115(DateTime date)
        {
            if (date >= DateTime.Today.AddYears(-115))
            {
                return false;
            }
            return true;
        }
    }
}
