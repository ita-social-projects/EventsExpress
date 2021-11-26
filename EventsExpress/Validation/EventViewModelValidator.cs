using System;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class EventViewModelValidator : AbstractValidator<EventViewModel>
    {
        public EventViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Title length exceeded the recommended length of 60 character!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateFrom).GreaterThan(DateTime.Today).WithMessage("Date from must be older than the current date!");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
            RuleFor(x => x.DateTo).GreaterThan(x => x.DateFrom).WithMessage("Date to must be older than date from!");
            RuleFor(x => x.Location).NotEmpty().OverridePropertyName("location").WithMessage("Field is required!");
            RuleFor(x => x.MaxParticipants).GreaterThan(0).WithMessage("Incorrect quantity of participants!");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Sellect at least 1 category");
            When(x => x.Location != null && x.Location.Type == LocationType.Online && x.Location.OnlineMeeting != null, () =>
            {
                RuleFor(x => x.Location.OnlineMeeting.ToString()).Must(LinkMustBeAUri).OverridePropertyName("location")
               .WithMessage("Link '{PropertyValue}' must be a valid URI. eg: http://www.SomeWebSite.com.au");
            });
            When(x => x.Location != null && x.Location.Type == LocationType.Online && x.Location.OnlineMeeting == null, () =>
            {
                RuleFor(x => x.Location.OnlineMeeting).NotEmpty().OverridePropertyName("location").WithMessage("Link is must not be empty");
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
