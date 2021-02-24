namespace EventsExpress.Validation.Base
{
    using System;
    using EventsExpress.Core.IServices;
    using EventsExpress.ViewModels.Base;
    using FluentValidation;

    public class BaseEventViewModelValidator<T> : AbstractValidator<T>
        where T : EventViewModelBase
    {
        public BaseEventViewModelValidator(ICategoryService categoryService)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Title length exceeded the recommended length of 60 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThanOrEqualTo(DateTime.Today).WithMessage("date from must be older than date now!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).GreaterThan(0).WithMessage("Input correct quantity of participants!");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Field is required!");
            RuleForEach(x => x.Categories).NotEmpty().WithMessage("Category can not be empty!");
            RuleForEach(x => x.Categories)
                .Must(x => categoryService.Exists(x.Id))
                .WithMessage("Invalid category id");
            When(x => x.IsReccurent, () =>
            {
                RuleFor(x => x.Frequency).GreaterThan(0).WithMessage("Frequency must be greater then 0!");
                RuleFor(x => x.Periodicity).IsInEnum().WithMessage("Field is required!");
            });
            RuleFor(x => x.Location).SetValidator(new LocationViewModelValidator());
        }
    }
}
