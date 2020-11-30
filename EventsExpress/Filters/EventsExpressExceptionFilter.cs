using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EventsExpress.Filters
{
    public class EventsExpressExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var result = new ViewResult { StatusCode = 400 };
            var modelMetadata = new EmptyModelMetadataProvider();
            result.ViewData = new ViewDataDictionary(
                    modelMetadata, context.ModelState);
            result.ViewData.Add(
                "HandleException",
                context.Exception);
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
