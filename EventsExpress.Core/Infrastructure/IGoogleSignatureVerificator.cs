using System.Threading.Tasks;
using Google.Apis.Auth;

namespace EventsExpress.Core.Infrastructure
{
    public interface IGoogleSignatureVerificator
    {
        Task<GoogleJsonWebSignature.Payload> Verify(string token);
    }
}
