using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.Services
{
    public class IpProviderService : IIpProviderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IpProviderService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetIpAdress()
        {
            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
            }

            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
