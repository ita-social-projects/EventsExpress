using System;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventViewModelValidator : AbstractValidator<EventViewModel>
    {
        public EventViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThanOrEqualTo(DateTime.Today).WithMessage("date from must be older than date now!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.User).NotNull().WithMessage("User does not exist!");
            RuleFor(x => x.City.Id).NotEqual(Guid.Empty).WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).GreaterThan(0).WithMessage("Input correct quantity of participants!");
        }
    }
}
