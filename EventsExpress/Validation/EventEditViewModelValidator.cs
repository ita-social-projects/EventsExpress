using System;
using EventsExpress.Validation.Base;
using FluentValidation;

namespace EventsExpress.ViewModels
{
    public class EventEditViewModelValidator
        : BaseEventViewModelValidator<EventEditViewModel>
    {
        public EventEditViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
        }
    }
}
