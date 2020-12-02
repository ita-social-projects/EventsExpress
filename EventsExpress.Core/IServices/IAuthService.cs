using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IAuthService
    {
        Task<AuthenticateResponseModel> Authenticate(string email, string password);

        Task<AuthenticateResponseModel> FirstAuthenticate(UserDTO userDto);

        Task ChangePasswordAsync(UserDTO userDto, string oldPassword, string newPassword);

        UserDTO GetCurrentUser(ClaimsPrincipal userClaims);

        Task<AuthenticateResponseModel> AuthenticateUserFromExternalProvider(string email);
    }
}
