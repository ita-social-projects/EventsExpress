using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;

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
            var id = _securityContextService.GetCurrentUserId();

            var att = source.Relationships?.FirstOrDefault(r => r.UserFromId == id)?.Attitude ?? Attitude.None;

            return (byte)att;
        }
    }
}
