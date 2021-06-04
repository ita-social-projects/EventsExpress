using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ValueResolvers
{
    public class ProfileToAttitudeResolver : IValueResolver<ProfileDto, ProfileViewModel, byte>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISecurityContext _securityContextService;

        public ProfileToAttitudeResolver(AppDbContext appDbContext, ISecurityContext securityContextService)
        {
            _appDbContext = appDbContext;
            _securityContextService = securityContextService;
        }

        public byte Resolve(ProfileDto source, ProfileViewModel destination, byte destMember, ResolutionContext context)
        {
            var currentUserId = _securityContextService.GetCurrentUserId();

            var res = _appDbContext.Relationships.FirstOrDefault(x => x.UserFromId == currentUserId && x.UserToId == destination.Id);

            return res != null ? (byte)res.Attitude : (byte)Attitude.None;
        }
    }
}
