using Microsoft.IdentityModel.Tokens;

namespace EventsExpress.Core.Infrastructure
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
