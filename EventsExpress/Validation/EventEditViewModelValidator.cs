using EventsExpress.Core.IServices;
using EventsExpress.Validation.Base;

namespace EventsExpress.ViewModels
{
    public class EventEditViewModelValidator
        : BaseEventViewModelValidator<EventEditViewModel>
    {
        public EventEditViewModelValidator(ICategoryService categoryService)
            : base(categoryService)
        {
        }
    }
}
