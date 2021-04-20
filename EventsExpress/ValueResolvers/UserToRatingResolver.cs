using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;

namespace EventsExpress.ValueResolvers
{
    public class UserToRatingResolver : IValueResolver<User, UserDto, double>
    {
        private readonly IUserService _userService;

        public UserToRatingResolver(IUserService userService)
        {
            _userService = userService;
        }

        public double Resolve(User source, UserDto destination, double destMember, ResolutionContext context)
        {
            var rating = _userService.GetRating(source.Id);

            return rating;
        }
    }
}
