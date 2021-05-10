using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IAuthService
    {
        Task<AuthenticateResponseModel> Authenticate(string email, string password);

        Task<AuthenticateResponseModel> Authenticate(string email, AuthExternalType type);

        Task<AuthenticateResponseModel> EmailConfirmAndAuthenticate(Guid authLocalId, string token);

        Task<bool> CanRegister(string email);

        Task<Guid> Register(RegisterDto registerDto);

        Task RegisterComplete(RegisterCompleteDto registerCompleteDto);

        Task PasswordRecover(string email);

        Task ChangePasswordAsync(ClaimsPrincipal userClaims, string oldPassword, string newPassword);

        UserDto GetCurrentUser(ClaimsPrincipal userClaims);

        Guid GetCurrentUserId(ClaimsPrincipal userClaims);

        Guid GetCurrentAccountId(ClaimsPrincipal userClaims);
    }
}
