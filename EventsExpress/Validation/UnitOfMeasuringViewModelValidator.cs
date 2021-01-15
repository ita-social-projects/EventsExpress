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

            RuleFor(x => x.UnitName).NotEmpty().Length(5, 20).WithMessage("Unit Name needs to consist of from 5 to 20 characters");

            RuleFor(x => x.ShortName).NotEmpty().Length(1, 5).WithMessage("Short Name needs to consist of from 1 to 5 characters");

            RuleFor(x => x)
               .Must(item => !_unitOfMeasuringService.ExistsByName(item.UnitName, item.ShortName))
               .WithMessage("The same UNIT OF MEASURING and SHORT UNIT OF MEASURING already exists!");

            RuleFor(x => x.UnitName).Cascade(CascadeMode.StopOnFirstFailure)
                .Matches(@"^[\p{L} ]+$")
                .WithMessage("Unit name needs to consist only letters or whitespaces");

            RuleFor(x => x.ShortName).Cascade(CascadeMode.StopOnFirstFailure)
                .Matches(@"^([\p{L}]+)([/]([\p{L}]+))?$")
                .WithMessage("Short name needs to consist only letters or letter(s)/letter(s)");
        }
    }
}
