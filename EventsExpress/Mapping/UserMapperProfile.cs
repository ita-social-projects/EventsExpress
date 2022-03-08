using System;
using System.Linq;
using System.Security;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ValueResolvers;
using EventsExpress.ViewModels;
using EventsExpress.ViewModels.Base;
using NetTopologySuite.Index.Strtree;

namespace EventsExpress.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories))
                .ForMember(dest => dest.NotificationTypes, opts => opts.MapFrom(src => src.NotificationTypes))
                .ForMember(dest => dest.Events, opts => opts.Ignore())
                .ForMember(dest => dest.Rating, opts => opts.MapFrom<UserToRatingResolver>())
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom<UserToAttitudeResolver>())
                .ForMember(dest => dest.CanChangePassword, opts => opts.MapFrom<UserChangePasswordResolver>())
                .ForMember(dest => dest.Location, opts => opts.MapFrom(srs => MapLocationUser(srs)))
                .ForMember(dest => dest.MyRates, opts => opts.Ignore())
                .ForMember(dest => dest.AccountId, opts => opts.MapFrom(src => src.Account.Id))
                .ForMember(
                    dest => dest.BookmarkedEvents,
                    opts => opts.MapFrom(src => src.EventBookmarks.Select(b => b.EventId)));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories))
                .ForMember(dest => dest.LocationId, opts => opts.Ignore())
                .ForMember(dest => dest.Location, opts => opts.Ignore())
                .ForMember(dest => dest.NotificationTypes, opts => opts.MapFrom(src => src.NotificationTypes))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<UserDto, UserInfoViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src =>
                    src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Account.AccountRoles.Select(x => x.Role.Name)))
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryViewModel
                        {
                            Id = x.Category.Id, Name = x.Category.Name,
                        })))
                .ForMember(
                    dest => dest.NotificationTypes,
                    opts => opts.MapFrom(src =>
                        src.NotificationTypes.Select(x =>
                            new NotificationTypeViewModel
                            {
                                Id = x.NotificationType.Id, Name = x.NotificationType.Name,
                            })))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.CanChangePassword, opts => opts.MapFrom(src => src.CanChangePassword))
                .ForMember(dest => dest.BookmarkedEvents, opts => opts.MapFrom(src => src.BookmarkedEvents))
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => MapLocationUser(src)));

            void MapUsername<T>(IMemberConfigurationExpression<UserDto, T, string> options)
            {
                options.MapFrom(source => source.Name ?? source.Email.Substring(
                    0, source.Email.IndexOf("@", StringComparison.Ordinal)));
            }

            CreateMap<UserDto, UserManageViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, MapUsername)
                .ForMember(dest => dest.IsBlocked, opts => opts.MapFrom(src => src.Account.IsBlocked))
                .ForMember(
                    dest => dest.Roles,
                    opts => opts.MapFrom(src => src.Account.AccountRoles.Select(x => new RoleViewModel
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name,
                    })));

            CreateMap<UserDto, UserShortInformationViewModel>()
                .ForMember(
                    destination => destination.Id,
                    options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.Username, MapUsername);

            CreateMap<UserDto, UserPreviewViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
                .ForMember(
                    dest => dest.Username,
                    opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.Rating, opts => opts.MapFrom(src => src.Rating))
                .ForMember(dest => dest.UserStatusEvent, opts => opts.Ignore())
                .ForMember(dest => dest.Attitude, opts => opts.Ignore());

            CreateMap<UserDto, ProfileDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name })))
                .ForMember(
                    dest => dest.NotificationTypes,
                    opts => opts.MapFrom(src =>
                        src.NotificationTypes.Select(x => new NotificationTypeDto { Id = x.NotificationType.Id, Name = x.NotificationType.Name })))
                .ForMember(
                    dest => dest.IsBlocked, opts => opts.MapFrom(src => src.Account.IsBlocked));

            CreateMap<ProfileDto, ProfileViewModel>()
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryViewModel { Id = x.Id, Name = x.Name })))
                .ForMember(
                    dest => dest.NotificationTypes,
                    opts => opts.MapFrom(src =>
                        src.NotificationTypes.Select(x => new NotificationTypeViewModel { Id = x.Id, Name = x.Name })))
                .ForMember(dest => dest.Rating, opts => opts.MapFrom<ProfileToRatingResolver>())
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom<ProfileToAttitudeResolver>());
        }

        private static LocationViewModel MapLocationUser(UserDto userDto)
        {
            return userDto.Location switch
            {
                { Type: LocationType.Map } => new LocationViewModel
                {
                    Latitude = userDto.Location.Point.X,
                    Longitude = userDto.Location.Point.Y,
                    OnlineMeeting = null,
                    Type = userDto.Location.Type,
                },
                { Type: LocationType.Online } => new LocationViewModel
                {
                    Latitude = null,
                    Longitude = null,
                    OnlineMeeting = userDto.Location.OnlineMeeting,
                    Type = userDto.Location.Type,
                },

                _ => null,
            };
        }

        private static LocationDto MapLocationUser(User u)
        {
            if (u.Location != null)
            {
                return new LocationDto
                {
                    Id = u.Id,
                    OnlineMeeting = u.Location.OnlineMeeting,
                    Point = u.Location.Point,
                    Type = u.Location.Type,
                };
            }

            return null;
        }
    }
}
