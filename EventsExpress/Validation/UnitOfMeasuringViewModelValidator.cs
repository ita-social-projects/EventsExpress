using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EventsExpress.Validation
{
    public class UnitOfMeasuringViewModelValidator : AbstractValidator<UnitOfMeasuringViewModel>
    {
        private readonly IUnitOfMeasuringService _unitOfMeasuringService;

        public UnitOfMeasuringViewModelValidator(IUnitOfMeasuringService unitOfMeasuringService)
        {
            _unitOfMeasuringService = unitOfMeasuringService;
            RuleFor(x => x.UnitName).NotEmpty().Length(5, 20);

            RuleFor(x => x.ShortName).NotEmpty().Length(1, 5);


            //RuleFor(x => x.UnitName).NotEmpty().Length(5, 20);

            //RuleFor(x => x.ShortName).NotEmpty().Length(2, 10);

            RuleFor(x => x)
               .Must(item => !_unitOfMeasuringService.ExistsByName(item.UnitName, item.ShortName))
               .WithMessage("The same UNIT OF MEASURING and SHORT UNIT OF MEASURING already exists!");

            RuleFor(x => x.UnitName).Cascade(CascadeMode.StopOnFirstFailure)
                 //.Matches(@"^[a-zA-Z]*$")
                 //.Matches(@"^[[\p{L}]+[\s]?]+$")
                 //.Matches(@"[\p{L} ]+$")
                .Matches(@"^[\p{L} ]+$")
                .WithMessage("Unit name needs to consist only characters");

            RuleFor(x => x.ShortName).Cascade(CascadeMode.StopOnFirstFailure)
                //.Matches(@"^([\p{L}]+)[/]?([\p{L}]+)+$")
                .Matches(@"^([\p{L}]+)([/]([\p{L}]+))?$")
                .WithMessage("Short name needs to consist only characters");
        }
    }
}
