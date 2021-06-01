using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.ValueResolvers
{
    public class UserChangePasswordResolver : IValueResolver<User, UserDto, bool>
    {
        private AppDbContext dbContext;
        private ISecurityContext securityContext;

        public UserChangePasswordResolver(AppDbContext dbContext, ISecurityContext securityContext)
        {
            this.dbContext = dbContext;
            this.securityContext = securityContext;
        }

        public bool Resolve(User source, UserDto destination, bool destMember, ResolutionContext context)
        {
            var user = dbContext.Users
                .Include(u => u.Account)
                    .ThenInclude(a => a.AuthLocal)
                .FirstOrDefault(x => x.Id == securityContext.GetCurrentUserId());

            if (user?.Account?.AuthLocal == null)
            {
                return false;
            }

            return true;
        }
    }
}
