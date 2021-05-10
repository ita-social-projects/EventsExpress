using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ValueResolvers
{
    public class UserToAttitudeResolver : IValueResolver<User, UserDto, byte>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IAuthService _authService;

        public UserToAttitudeResolver(
            IHttpContextAccessor httpContextAccessor,
            IAuthService authService)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public byte Resolve(User source, UserDto destination, byte destMember, ResolutionContext context)
        {
            var id = _authService.GetCurrentUserId(_httpContextAccessor.HttpContext.User);

            var att = source.Relationships?.FirstOrDefault(r => r.UserFromId == id)?.Attitude ?? Attitude.None;

            return (byte)att;
        }
    }
}
