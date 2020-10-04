using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using Google.Apis.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IAuthService
    {

        Task<(OperationResult opResult, AuthenticateResponseModel authResponseModel)> Authenticate(string email, string password);
        Task<(OperationResult opResult, AuthenticateResponseModel authResponseModel)> FirstAuthenticate(UserDTO userDto);
        Task<OperationResult> ChangePasswordAsync(UserDTO userDto, string oldPassword, string newPassword);
        UserDTO GetCurrentUser(ClaimsPrincipal userClaims);
        OperationResult AuthenticateGoogleFacebookUser(string email, out AuthenticateResponseModel responseModel);



    }
}
