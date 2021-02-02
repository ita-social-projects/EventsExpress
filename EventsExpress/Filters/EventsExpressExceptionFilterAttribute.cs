using System;
using System.Collections.Generic;
using EventsExpress.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventsExpress.Filters
{
    public class EventsExpressExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is EventsExpressException eventsExpressException)
            {
                var errorTexts = new
                {
                    Errors = new Dictionary<string, Array>()
                    {
                        { string.Empty, new[] { eventsExpressException.Message } },
                    },
                };
                var result = new ObjectResult(errorTexts) { StatusCode = 400 };
                context.Result = result;
                context.ExceptionHandled = true;
            }
            else
            {
                string message = "Unhandled exception occurred. Please try again. " +
                    "If this error persists - contact system administrator.";
                context.ModelState.AddModelError(string.Empty, message);
                var result = new ObjectResult(context.ModelState) { StatusCode = 500 };
                context.Result = result;
                context.ExceptionHandled = true;
            }
        }
    }
}
