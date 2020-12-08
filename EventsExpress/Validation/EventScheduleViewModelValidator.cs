using System;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventScheduleViewModelValidator : AbstractValidator<PreviewEventScheduleViewModel>
    {
        public EventScheduleViewModelValidator()
        {
            RuleFor(x => x.LastRun).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.NextRun).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Periodicity).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Frequency).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.EventId).NotEqual(Guid.Empty).WithMessage("Field is required!");
        }
    }
}
