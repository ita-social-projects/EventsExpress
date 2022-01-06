using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;

namespace EventsExpress.ValueResolvers
{
    public class UserChangePasswordResolver : IValueResolver<User, UserDto, bool>
    {
        private readonly AppDbContext dbContext;
        private readonly ISecurityContext securityContext;

        public UserChangePasswordResolver(AppDbContext dbContext, ISecurityContext securityContext)
        {
            this.dbContext = dbContext;
            this.securityContext = securityContext;
        }

        public bool Resolve(User source, UserDto destination, bool destMember, ResolutionContext context)
        {
            try
            {
                var currentAccountId = securityContext.GetCurrentAccountId();
                return dbContext.AuthLocal.Any(x => x.AccountId == currentAccountId);
            }
            catch (Core.Exceptions.EventsExpressException)
            {
                return false;
            }
        }
    }
}
