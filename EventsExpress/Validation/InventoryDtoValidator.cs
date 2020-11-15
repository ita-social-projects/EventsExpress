using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class InventoryDtoValidator : AbstractValidator<InventoryDto>
    {
        public InventoryDtoValidator()
        {
            RuleFor(x => x.ItemName).NotEmpty().Length(1, 30);
            RuleFor(x => x.NeedQuantity).GreaterThan(0);
            RuleFor(x => x.UnitOfMeasuring.Id).NotEmpty();
        }
    }
}