using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class UnitOfMeasuringCreateViewModelValidator : AbstractValidator<UnitOfMeasuringCreateViewModel>
    {
        private readonly IUnitOfMeasuringService _unitOfMeasuringService;

        public UnitOfMeasuringCreateViewModelValidator(IUnitOfMeasuringService unitOfMeasuringService)
        {
            _unitOfMeasuringService = unitOfMeasuringService;

            RuleFor(x => x.UnitName).NotEmpty().WithMessage("Unit name should not be empty")
                .Length(5, 20).WithMessage("Unit name should consist of from 5 to 20 characters");

            RuleFor(x => x.ShortName).NotEmpty().WithMessage("Short name should not be empty")
                .Length(1, 5).WithMessage("Short name should consist of from 1 to 5 characters");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category should be selected");

            RuleFor(x => x)
               .Must(item => !_unitOfMeasuringService.ExistsByItems(item.UnitName, item.ShortName, item.CategoryId))
               .WithMessage("The same UNIT OF MEASURING already exists!");

            RuleFor(x => x.UnitName).Cascade(CascadeMode.Stop)
                .Matches(@"^[\p{L} ]+$")
                .WithMessage("Unit name should consist only letters or whitespaces");

            RuleFor(x => x.ShortName).Cascade(CascadeMode.Stop)
                .Matches(@"^([\p{L}]+)([/]([\p{L}]+))?$")
                .WithMessage("Short name should consist only letters");
        }
    }
}
