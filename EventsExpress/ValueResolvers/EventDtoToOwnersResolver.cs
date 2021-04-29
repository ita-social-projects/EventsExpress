using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using EventsExpress.ViewModels.Base;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.ValueResolvers
{
    public class EventDtoToOwnersResolver : IValueResolver<EventDto, EventViewModelBase, IEnumerable<UserPreviewViewModel>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public EventDtoToOwnersResolver(
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public IEnumerable<UserPreviewViewModel> Resolve(EventDto source, EventViewModelBase destination, IEnumerable<UserPreviewViewModel> destMember, ResolutionContext context)
        {
            var res = new List<UserPreviewViewModel>();
            UserDto currUser = null;
            try
            {
                currUser = _authService?.GetCurrentUser(_httpContextAccessor.HttpContext.User);
            }
            catch (EventsExpressException)
            {
            }

            foreach (var o in source.Owners)
            {
                var att = o.Relationships?.FirstOrDefault(r => r.UserFromId == currUser?.Id)?.Attitude ?? Attitude.None;

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
