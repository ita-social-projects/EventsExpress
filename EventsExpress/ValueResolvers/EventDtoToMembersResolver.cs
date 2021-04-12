using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ValueResolvers
{
    public class EventDtoToMembersResolver : IValueResolver<EventDto, EventPreviewViewModel, IEnumerable<UserPreviewViewModel>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public EventDtoToMembersResolver(
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public IEnumerable<UserPreviewViewModel> Resolve(EventDto source, EventPreviewViewModel destination, IEnumerable<UserPreviewViewModel> destMember, ResolutionContext context)
        {
            var res = new List<UserPreviewViewModel>();

            var currUser = _authService?.GetCurrentUser(_httpContextAccessor.HttpContext.User);

            foreach (var u in source.Visitors)
            {
                var att = u.User.Relationships?.FirstOrDefault(r => r.UserFromId == currUser?.Id)?.Attitude ?? Attitude.None;

                res.Add(new UserPreviewViewModel
                {
                    Id = u.User.Id,
                    Username = u.User.Name ?? u.User.Email.Substring(0, u.User.Email.IndexOf("@")),
                    Birthday = u.User.Birthday,
                    UserStatusEvent = u.UserStatusEvent,
                    Rating = _userService.GetRating(u.User.Id),
                    Attitude = att,
                });
            }

            return res;
        }
    }
}
