using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using FluentValidation;

namespace EventsExpress.Validation
{
    public class RateDtoValidator : AbstractValidator<RateDto>
    {
        private readonly IEventService _eventService;

        public RateDtoValidator(IEventService eventSrv)
        {
            _eventService = eventSrv;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.EventId)
                .NotEqual(Guid.Empty)
                .Must(id => _eventService.Exists(id))
                .WithMessage("Invalid eventId");

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid userId");
            
            RuleFor(x => x)
                .Must(x => _eventService.UserIsVisitor(x.UserId, x.EventId))
                .WithMessage("User isn't visitor");

            RuleFor(x => x.Rate)
                .InclusiveBetween((byte)0, (byte)10)
                .WithMessage("Invalid rate");
        }
    }
}
