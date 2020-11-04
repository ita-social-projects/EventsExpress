using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EventsExpress.Core.Extensions
{
    public static class HttpContextExtensions
    {
        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            AppHttpContext.Configure(app.ApplicationServices
                .GetRequiredService<IHttpContextAccessor>());
            return app;
        }
    }
}
