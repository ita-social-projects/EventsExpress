namespace EventsExpress.Validation.Base
{
    using System;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels.Base;
    using FluentValidation;

    public class LocationViewModelValidator : AbstractValidator<MapViewModel>
    {
        public LocationViewModelValidator()
        {
        }
    }
}
