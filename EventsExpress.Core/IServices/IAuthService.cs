using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface IAuthService
    {
        Task<AuthenticateResponseModel> Authenticate(string email, string password);

        Task<AuthenticateResponseModel> FirstAuthenticate(UserDto userDto);

        Task ChangePasswordAsync(UserDto userDto, string oldPassword, string newPassword);

        UserDto GetCurrentUser(ClaimsPrincipal userClaims);

        User GetCurrUserId(ClaimsPrincipal userClaims);

        Task<AuthenticateResponseModel> AuthenticateUserFromExternalProvider(string email);
    }
}
