using System;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class UserEventInventoryViewModelValidator : AbstractValidator<UserEventInventoryViewModel>
    {
        public UserEventInventoryViewModelValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity should be greater than 0");
            RuleFor(x => x.EventId).NotEqual(Guid.Empty).WithMessage("Field is required!");
            RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("Field is required!");
            RuleFor(x => x.InventoryId).NotEqual(Guid.Empty).WithMessage("Field is required!");
        }
    }
}
