using System;
using EventsExpress.Db.Entities;
using EventsExpress.Validation.Base;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class LocationValidator : AbstractValidator<EventLocation>
    {
        public LocationValidator()
        {
            When(x => x.Type == Db.Enums.LocationType.Online, () =>
            {
                RuleFor(x => x.OnlineMeeting.ToString()).Must(LinkMustBeAUri)
               .WithMessage("Link '{PropertyValue}' must be a valid URI. eg: http://www.SomeWebSite.com.au");
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
