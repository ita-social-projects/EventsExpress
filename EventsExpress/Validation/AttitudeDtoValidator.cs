using System;
using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class AttitudeDtoValidator : AbstractValidator<AttitudeDto>
    {
        public AttitudeDtoValidator()
        {
            RuleFor(x => x.UserFromId).NotEqual(Guid.Empty).WithMessage("Id can not be null!");
            RuleFor(x => x.UserToId).NotEqual(Guid.Empty).WithMessage("Id can not be null!");
            
        }
    }
}
