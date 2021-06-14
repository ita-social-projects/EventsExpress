using System;
using EventsExpress.Db.Entities;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Title length exceeded the recommended length of 60 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThan(DateTime.Today).WithMessage("Date from must be older than the current date!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateTo).GreaterThan(x => x.DateFrom).WithMessage("Date to must be older than date from!");
            RuleFor(x => x.EventLocation).NotEmpty().OverridePropertyName("location.type").WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).GreaterThan(0).WithMessage("Incorrect quantity of participants!");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Sellect at least 1 category");
            When(x => x.EventSchedule != null, () =>
            {
                RuleFor(x => x.EventSchedule.Frequency).GreaterThan(0).OverridePropertyName("frequency").WithMessage("Incorrect frequency!");
            });
        }
    }
}
