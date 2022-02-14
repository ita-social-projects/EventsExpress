using System;
using EventsExpress.Core.Extensions;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EditUserBirthViewModelValidator : AbstractValidator<EditUserBirthViewModel>
    {
        private const int MinAge = 14;
        private const int MaxAge = 115;

        public EditUserBirthViewModelValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Birthday)
                .Must(NotSpecifyTime)
                    .WithName(x => nameof(x.Birthday))
                    .WithMessage("{PropertyName} must be a date, but time was specified")
                .Must(NotBeInTheFuture)
                    .WithMessage("{PropertyName} must not be a future date")
                .Must(BeMinAgeOrOver)
                    .WithMessage($"{{PropertyName}} must be {MinAge} years or over from today")
                .Must(BeLessThanMaxAge)
                    .WithMessage($"{{PropertyName}} must be less than {MaxAge} years from today");
        }

        private bool NotSpecifyTime(DateTime date)
            => date.TimeOfDay.TotalSeconds == 0;

        private bool NotBeInTheFuture(DateTime date)
            => date <= DateTime.Today;

        private bool BeMinAgeOrOver(DateTime date)
            => DateTime.Today.GetDifferenceInYears(date) >= MinAge;

        private bool BeLessThanMaxAge(DateTime date)
            => DateTime.Today.GetDifferenceInYears(date) < MaxAge;
    }
}
