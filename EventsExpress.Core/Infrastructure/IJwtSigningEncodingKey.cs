using Microsoft.IdentityModel.Tokens;

namespace EventsExpress.Core.Infrastructure
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
