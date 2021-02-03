using System;
using EventsExpress.Core.IServices;
using EventsExpress.Validation.Base;
using FluentValidation;

namespace EventsExpress.ViewModels
{
    public class EventCreateViewModelValidator
        : BaseEventViewModelValidator<EventCreateViewModel>
    {
        public EventCreateViewModelValidator(ICategoryService categoryService)
            : base(categoryService)
        {
        }
    }
}
