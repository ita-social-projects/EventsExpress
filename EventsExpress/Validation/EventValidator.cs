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
            RuleFor(x => x.DateFrom).GreaterThanOrEqualTo(DateTime.Today).WithMessage("date from must be older than date now!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateTo).GreaterThanOrEqualTo(x => x.DateFrom).WithMessage("date from must be older than date from!");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Sellect at least 1 category");
            RuleFor(x => x.EventLocation).NotEmpty().OverridePropertyName("location.type").WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).GreaterThan(0).WithMessage("Incorrect quantity of participants!");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Sellect at least 1 category");
        }
    }
}
