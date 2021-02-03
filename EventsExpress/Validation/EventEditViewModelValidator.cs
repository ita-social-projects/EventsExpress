using System;
using EventsExpress.Core.IServices;
using EventsExpress.Validation.Base;
using FluentValidation;

namespace EventsExpress.ViewModels
{
    public class EventEditViewModelValidator
        : BaseEventViewModelValidator<EventEditViewModel>
    {
        public EventEditViewModelValidator(ICategoryService categoryService)
            : base(categoryService)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
        }
    }
}
