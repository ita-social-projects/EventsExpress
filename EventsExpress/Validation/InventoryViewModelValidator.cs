using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class InventoryViewModelValidator : AbstractValidator<InventoryViewModel>
    {
        public InventoryViewModelValidator()
        {
            RuleFor(x => x.ItemName).NotEmpty().Length(1, 30);
            RuleFor(x => x.NeedQuantity).GreaterThan(0);
        }
    }
}
