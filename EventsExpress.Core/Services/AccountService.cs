using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        private readonly IMediator _mediator;

        public AccountService(
            AppDbContext context,
            IMediator mediator)
            : base(context)
        {
            _mediator = mediator;
        }

        public async Task AddAuth(Guid accountId, string email, AuthExternalType type)
        {
            var exist = await Context.AuthExternal.AnyAsync(ae => ae.Email == email && ae.Type == type);
            if (exist)
            {
                throw new EventsExpressException("This login already in use");
            }

            var auth = new AuthExternal
            {
                AccountId = accountId,
                Email = email,
                Type = type,
            };
            Context.AuthExternal.Add(auth);
            await Context.SaveChangesAsync();
        }

        public async Task AddAuth(Guid accountId, string email, string password)
        {
            var exist = await Context.AuthLocal.AnyAsync(ae => ae.Email == email);
            if (exist)
            {
                throw new EventsExpressException("This login already in use");
            }

            string salt = PasswordHasher.GenerateSalt();
            var auth = new AuthLocal
            {
                AccountId = accountId,
                Email = email,
                Salt = salt,
                PasswordHash = PasswordHasher.GenerateHash(password, salt),
            };
            Context.AuthLocal.Add(auth);
            await Context.SaveChangesAsync();

            await _mediator.Publish(new RegisterVerificationMessage(auth));
        }

        public async Task EnsureExternalAccountAsync(string email, AuthExternalType type)
        {
            var exist = await Context.AuthExternal.AnyAsync(ae => ae.Email == email && ae.Type == type);
            if (exist)
            {
                return;
            }

            var account = new Account()
            {
                AuthExternal = new[]
                {
                    new AuthExternal
                    {
                        Email = email,
                        Type = type,
                    },
                },
                AccountRoles = new[]
                {
                    new AccountRole { RoleId = Db.Enums.Role.User },
                },
            };
            Context.Accounts.Add(account);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthDto>> GetLinkedAuth(Guid accountId)
        {
            var authE = Context.AuthExternal
                .Where(a => a.AccountId == accountId)
                .Select(a => new AuthDto
                {
                    Email = a.Email,
                    Type = a.Type,
                });

            var authL = Context.AuthLocal
                .Where(a => a.AccountId == accountId)
                .Select(a => new AuthDto
                {
                    Email = a.Email,
                    Type = null,
                });

            var auths = await authE.Union(authL).ToListAsync();
            return auths;
        }
    }
}
