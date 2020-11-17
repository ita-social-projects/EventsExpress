using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class UnitOfMeasuringDtoValidator : AbstractValidator<UnitOfMeasuringDto>
    {
        public UnitOfMeasuringDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ShortName).NotEmpty();
            RuleFor(x => x.UnitName).NotEmpty();
        }
    }
}
