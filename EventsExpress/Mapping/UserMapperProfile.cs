using System;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

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
                .ForMember(dest => dest.Rating, opts => opts.Ignore())
                .ForMember(dest => dest.Attitude, opts => opts.Ignore())
                .ForMember(dest => dest.CanChangePassword, opts => opts.Ignore())
                .ForMember(dest => dest.MyRates, opts => opts.Ignore())
                .ForMember(dest => dest.AccountId, opts => opts.MapFrom(src => src.Account.Id));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhotoId, opts => opts.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories))
                 .ForMember(dest => dest.NotificationTypes, opts => opts.MapFrom(src => src.NotificationTypes))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<UserDto, UserInfoViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Account.AccountRoles.Select(x => x.Role.Name)))
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryViewModel { Id = x.Category.Id, Name = x.Category.Name })))
                .ForMember(
                    dest => dest.NotificationTypes,
                    opts => opts.MapFrom(src =>
                        src.NotificationTypes.Select(x => new NotificationTypeViewModel { Id = x.NotificationType.Id, Name = x.NotificationType.Name })))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender));

            CreateMap<UserDto, UserManageViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.IsBlocked, opts => opts.MapFrom(src => src.Account.IsBlocked))
                .ForMember(
                    dest => dest.Roles,
                    opts => opts.MapFrom(src => src.Account.AccountRoles.Select(x => new RoleViewModel
                    {
                        Id = x.Role.Id,
                        Name = x.Role.Name,
                    })))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()));

            CreateMap<UserDto, UserPreviewViewModel>()
                .ForMember(
                    dest => dest.Username,
                    opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.UserStatusEvent, opts => opts.Ignore());

            CreateMap<UserDto, ProfileDto>()
                .ForMember(
                    dest => dest.UserPhoto,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name })))
                .ForMember(
                    dest => dest.NotificationTypes,
                    opts => opts.MapFrom(src =>
                        src.NotificationTypes.Select(x => new NotificationTypeDto { Id = x.NotificationType.Id, Name = x.NotificationType.Name })));

            CreateMap<ProfileDto, ProfileViewModel>()
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryViewModel { Id = x.Id, Name = x.Name })))
                .ForMember(
                    dest => dest.NotificationTypes,
                    opts => opts.MapFrom(src =>
                        src.NotificationTypes.Select(x => new NotificationTypeViewModel { Id = x.Id, Name = x.Name })));
        }
    }
}
