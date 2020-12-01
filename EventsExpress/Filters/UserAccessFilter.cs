using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventsExpress.ActionFilters
{
    public class UserAccessFilter : ActionFilterAttribute
    {
        private readonly IEventService _eventService;

        public UserAccessFilter(IEventService eventService, string eventId)
        {
            _eventService = eventService;
            EventId = eventId;
        }

        public string EventId { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var evId = GetEventId(context, EventId);

            var ev = _eventService.EventById(evId);

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ObjectResult("User isn't authenticated!")
                {
                    StatusCode = 401,
                };
            }

            if (context.HttpContext.User.Identity.Name != ev.Owner.Id.ToString())
            {
                context.Result = new ObjectResult("User hasn't permission for this action!")
                {
                    StatusCode = 403,
                };
            }
        }

        private Guid GetEventId(ActionExecutingContext context, string id)
        {
            switch (id)
            {
                case "model":
                    EventViewModel model = (EventViewModel)context.ActionArguments[EventId];
                    return model.Id;
                case "eventId":
                    return new Guid(context.ActionArguments[EventId].ToString());
                default:
                    return Guid.Empty;
            }
        }
    }
}
