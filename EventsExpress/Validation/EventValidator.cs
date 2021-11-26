using System;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Title length exceeded the recommended length of 60 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThan(DateTime.Today).WithMessage("Date from must be older than the current date!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateTo).GreaterThan(x => x.DateFrom).WithMessage("Date to must be older than date from!");
            RuleFor(x => x.EventLocation).NotEmpty().OverridePropertyName("location").WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).GreaterThan(0).WithMessage("Incorrect quantity of participants!");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Sellect at least 1 category");
            When(x => x.EventSchedule != null, () =>
            {
                RuleFor(x => x.EventSchedule.Frequency).GreaterThan(0).OverridePropertyName("frequency").WithMessage("Incorrect frequency!");
            });
            When(x => x.EventLocation != null && x.EventLocation.Type == LocationType.Online && x.EventLocation.OnlineMeeting != null, () =>
            {
                RuleFor(x => x.EventLocation.OnlineMeeting.ToString()).Must(LinkMustBeAUri).OverridePropertyName("location")
               .WithMessage("Link '{PropertyValue}' must be a valid URI. eg: http://www.SomeWebSite.com.au");
            });
            When(x => x.EventLocation != null && x.EventLocation.Type == LocationType.Online && x.EventLocation.OnlineMeeting == null, () =>
            {
                RuleFor(x => x.EventLocation.OnlineMeeting).NotEmpty().OverridePropertyName("location").WithMessage("Link is must not be empty");
            });
        }

        private bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            return Uri.TryCreate(link, UriKind.Absolute, out Uri outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
