using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IAccountService
    {
        Task AddAuth(Guid accountId, string email, AuthExternalType type);

        Task AddAuth(Guid accountId, string email, string password);

        Task EnsureExternalAccountAsync(string email, AuthExternalType type);

        Task<IEnumerable<AuthDto>> GetLinkedAuth(Guid accountId);

        Task ChangeRole(Guid userId, Guid roleId);

        Task Block(Guid userId);

        Task Unblock(Guid userId);
    }
}
