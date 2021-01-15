using System;
using EventsExpress.ViewModels.Base;
using FluentValidation;

namespace EventsExpress.Validation.Base
{
    public class BaseEventViewModelValidator<T> : AbstractValidator<T>
        where T : EventViewModelBase
    {
        public BaseEventViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Title length exceeded the recommended length of 60 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThanOrEqualTo(DateTime.Today).WithMessage("date from must be older than date now!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Latitude).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Longitude).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).GreaterThan(0).WithMessage("Input correct quantity of participants!");
        }
    }
}
