using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IAuthServicre
    {
        OperationResult Authenticate(string email, string password);
        OperationResult FirstAuthenticate(UserDTO userDto);
        Task<OperationResult> ChangePasswordAsync(UserDTO userDto, string oldPassword, string newPassword);
        UserDTO GetCurrentUser(ClaimsPrincipal userClaims);
    }
}
