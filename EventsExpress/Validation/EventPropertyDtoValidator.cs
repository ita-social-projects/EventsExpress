using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventPropertyDtoValidator : AbstractValidator<EventDto>
    {
        public EventPropertyDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Title length exceeded the recommended length of 60 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThan(DateTime.Today).WithMessage("Date from must be older than the current date!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateTo).GreaterThan(x => x.DateFrom).WithMessage("Date to must be older than date from!");
        }
    }
}
