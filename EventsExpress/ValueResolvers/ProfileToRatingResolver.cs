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
    public class ProfileToRatingResolver : IValueResolver<ProfileDto, ProfileViewModel, double>
    {
        private readonly IUserService _userService;

        public ProfileToRatingResolver(IUserService userService)
        {
            _userService = userService;
        }

        public double Resolve(ProfileDto source, ProfileViewModel destination, double destMember, ResolutionContext context)
        {
            var rating = _userService.GetRating(source.Id);

            return rating;
        }
    }
}
