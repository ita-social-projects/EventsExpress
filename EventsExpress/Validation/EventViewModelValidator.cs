using System;
using EventsExpress.Db.Enums;
using EventsExpress.Validation.Base;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventViewModelValidator : AbstractValidator<EventViewModel>
    {
        public EventViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Title length exceeded the recommended length of 60 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThan(DateTime.Today).WithMessage("Date from must be older than the current date!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateTo).GreaterThan(x => x.DateFrom).WithMessage("Date to must be older than date from!");
            RuleFor(x => x.Location).NotEmpty().OverridePropertyName("location").WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).GreaterThan(0).WithMessage("Incorrect quantity of participants!");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Sellect at least 1 category");
            RuleFor(x => x.Location).SetValidator(new LocationViewModelValidator());
        }
    }
}
