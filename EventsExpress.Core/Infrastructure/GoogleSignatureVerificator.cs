using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using Google.Apis.Auth;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace EventsExpress.Core.Infrastructure
{
    public class GoogleSignatureVerificator : IGoogleSignatureVerificator
    {
        [ExcludeFromCodeCoverage]
        public Task<Payload> Verify(string token)
        {
            try
            {
                return ValidateAsync(token, new ValidationSettings());
            }
            catch (InvalidJwtException e)
            {
                throw new EventsExpressException(e.Message);
            }
        }
    }
}
