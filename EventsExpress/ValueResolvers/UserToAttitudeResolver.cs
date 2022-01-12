using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.ValueResolvers
{
    public class UserToAttitudeResolver : IValueResolver<User, UserDto, byte>
    {
        private readonly ISecurityContext _securityContextService;

        public UserToAttitudeResolver(ISecurityContext securityContextService)
        {
            _securityContextService = securityContextService;
        }

        public byte Resolve(User source, UserDto destination, byte destMember, ResolutionContext context)
        {
            try
            {
                var id = _securityContextService.GetCurrentUserId();
                var att = source.Relationships?.FirstOrDefault(r => r.UserFromId == id)?.Attitude ?? Attitude.None;
                return (byte)att;
            }
            catch (Core.Exceptions.EventsExpressException)
            {
                return (byte)Attitude.None;
            }
        }
    }
}
