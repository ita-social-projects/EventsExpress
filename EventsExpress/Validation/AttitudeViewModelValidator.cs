using System;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class AttitudeViewModelValidator : AbstractValidator<AttitudeViewModel>
    {
        public AttitudeViewModelValidator()
        {
            RuleFor(x => x.UserFromId).NotEqual(Guid.Empty).WithMessage("Id can not be null!");
            RuleFor(x => x.UserToId).NotEqual(Guid.Empty).WithMessage("Id can not be null!");
        }
    }
}
