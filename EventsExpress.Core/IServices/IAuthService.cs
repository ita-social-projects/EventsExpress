using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IAuthService
    {
        Task<AuthenticateResponseModel> Authenticate(string email, string password);

        Task<AuthenticateResponseModel> FirstAuthenticate(Guid authLocalId, string token);

        Task<Guid> Register(RegisterDto registerDto);

        Task CompleteRegistration(CompleteRegistrationDto completeRegistrationDto);

        Task ChangeRole(Guid userId, Guid roleId);

        Task PasswordRecover(string email);

        Task<bool> CanRegister(string email);

        Task Block(Guid userId);

        Task Unblock(Guid userId);

        Task ChangePasswordAsync(ClaimsPrincipal userClaims, string oldPassword, string newPassword);

        UserDto GetCurrentUser(ClaimsPrincipal userClaims);

        Task<AuthenticateResponseModel> AuthenticateUserFromExternalProvider(string email, AuthExternalType type);
    }
}
