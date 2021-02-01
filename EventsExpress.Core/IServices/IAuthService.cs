using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IAuthService
    {
        Task<AuthenticateResponseModel> Authenticate(string email, string password);

        Task<AuthenticateResponseModel> FirstAuthenticate(UserDto userDto);

        Task ChangePasswordAsync(UserDto userDto, string oldPassword, string newPassword);

        UserDto GetCurrentUser(ClaimsPrincipal userClaims);

        Task<AuthenticateResponseModel> AuthenticateUserFromExternalProvider(string email);
    }
}
