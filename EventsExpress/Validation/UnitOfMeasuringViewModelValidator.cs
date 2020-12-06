using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class UnitOfMeasuringViewModelValidator : AbstractValidator<UnitOfMeasuringViewModel>
    {
        public UnitOfMeasuringViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ShortName).NotEmpty();
            RuleFor(x => x.UnitName).NotEmpty();
        }
    }
}
