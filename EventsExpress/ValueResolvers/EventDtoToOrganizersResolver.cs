using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using EventsExpress.ViewModels.Base;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ValueResolvers
{
    public class EventDtoToOrganizersResolver : IValueResolver<EventDto, EventViewModelBase, IEnumerable<UserPreviewViewModel>>
    {
        private readonly IUserService _userService;
        private readonly ISecurityContext _securityContextService;

        public EventDtoToOrganizersResolver(
            IUserService userService,
            ISecurityContext securityContextService)
        {
            _userService = userService;
            _securityContextService = securityContextService;
        }

        public IEnumerable<UserPreviewViewModel> Resolve(EventDto source, EventViewModelBase destination, IEnumerable<UserPreviewViewModel> destMember, ResolutionContext context)
        {
            var res = new List<UserPreviewViewModel>();
            Guid? currentUserId;
            try
            {
                currentUserId = _securityContextService.GetCurrentUserId();
            }
            catch (EventsExpressException)
            {
                currentUserId = null;
            }

            foreach (var o in source.Owners)
            {
                var att = o.Relationships?.FirstOrDefault(r => r.UserFromId == currentUserId)?.Attitude ?? Attitude.None;

                res.Add(new UserPreviewViewModel
                {
                    Id = o.Id,
                    Username = o.Name ?? o.Email.Substring(0, o.Email.IndexOf("@")),
                    Birthday = o.Birthday,
                    Rating = _userService.GetRating(o.Id),
                    Attitude = att,
                });
            }

            return res;
        }
    }
}
