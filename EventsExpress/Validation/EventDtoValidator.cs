using System;
using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventDtoValidator : AbstractValidator<EventDto>
    {
        public EventDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThanOrEqualTo(DateTime.Today).WithMessage("date from must be older than date now!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.CityId).NotEqual(Guid.Empty).WithMessage("Field is required!");
        }
    }
}