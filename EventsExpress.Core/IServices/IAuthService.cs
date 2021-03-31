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

        Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal userClaims);

        Task<AuthenticateResponseModel> EmailConfirmAndAuthenticate(Guid authLocalId, string token);

        Task<Guid> Register(RegisterDto registerDto);

        Task RegisterComplete(RegisterCompleteDto registerCompleteDto);

        Task ChangeRole(Guid userId, Guid roleId); // to do

        Task PasswordRecover(string email);

        Task<bool> CanRegister(string email);

        Task Block(Guid userId); // to do

        Task Unblock(Guid userId); // to do

        Task ChangePasswordAsync(ClaimsPrincipal userClaims, string oldPassword, string newPassword);

        UserDto GetCurrentUser(ClaimsPrincipal userClaims);

        Task<AuthenticateResponseModel> AuthenticateUserFromExternalProvider(string email, AuthExternalType type);
    }
}
