using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserDTO user);

        RefreshToken GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromJwt(string token);

        Task<AuthenticateResponseModel> RefreshToken(string token);

        Task<bool> RevokeToken(string token);

        void SetTokenCookie(string token);
    }
}
