using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        private readonly IMediator _mediator;
        private readonly IPasswordHasher _passwordHasher;

        public AccountService(
            AppDbContext context,
            IMediator mediator,
            IMapper mapper,
            IPasswordHasher passwordHasher)
            : base(context, mapper)
        {
            _mediator = mediator;
            _passwordHasher = passwordHasher;
        }

        public async Task ChangeRole(Guid userId, IEnumerable<Db.Entities.Role> roles)
        {
            if (roles.CollectionIsNullOrEmpty())
            {
                throw new EventsExpressException("Invalid Roles");
            }

            var account = Context.Accounts
                .Include(a => a.AccountRoles)
                .SingleOrDefault(a => a.UserId == userId);

            if (account == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            var newRoles = roles
                .Select(role => new AccountRole { AccountId = account.Id, RoleId = role.Id })
                .ToList();

            account.AccountRoles = newRoles;

            Update(account);
            await Context.SaveChangesAsync();
            await _mediator.Publish(new UserRoleChangedMessage());
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

            string salt = _passwordHasher.GenerateSalt();
            var auth = new AuthLocal
            {
                AccountId = accountId,
                Email = email,
                Salt = salt,
                PasswordHash = _passwordHasher.GenerateHash(password, salt),
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
                .ProjectTo<AuthDto>(Mapper.ConfigurationProvider);

            var authL = Context.AuthLocal
                .Where(a => a.AccountId == accountId)
                .ProjectTo<AuthDto>(Mapper.ConfigurationProvider);

            var auths = authE.Union(authL);
            return await auths.ToListAsync();
        }

        public async Task Block(Guid userId)
        {
            var account = Context.Accounts.FirstOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            account.IsBlocked = true;
            await Context.SaveChangesAsync();
            await _mediator.Publish(new BlockedAccountMessage(account));
        }

        public async Task Unblock(Guid userId)
        {
            var account = Context.Accounts.FirstOrDefault(a => a.UserId == userId);
            if (account == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            account.IsBlocked = false;
            await Context.SaveChangesAsync();
            await _mediator.Publish(new UnblockedAccountMessage(account));
        }
    }
}
