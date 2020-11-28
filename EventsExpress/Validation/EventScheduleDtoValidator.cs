using System;
using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventScheduleDtoValidator : AbstractValidator<EventScheduleDto>
    {
        public EventScheduleDtoValidator()
        {
            RuleFor(x => x.LastRun).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.NextRun).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Periodicity).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Frequency).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Event).NotNull().WithMessage("Event is required!");
            RuleFor(x => x.EventId).NotEqual(Guid.Empty).WithMessage("Field is required!");
        }
    }
}
