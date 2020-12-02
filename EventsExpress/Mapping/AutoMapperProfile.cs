using System;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EventsExpress.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region USER MAPPING
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories))
                .ForMember(dest => dest.Events, opts => opts.Ignore())
                .ForMember(dest => dest.RefreshTokens, opts => opts.MapFrom(src => src.RefreshTokens));

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.EmailConfirmed, opts => opts.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.IsBlocked, opts => opts.MapFrom(src => src.IsBlocked))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opts => opts.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.PhotoId, opts => opts.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.RoleId, opts => opts.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.RefreshTokens, opts => opts.MapFrom(src => src.RefreshTokens))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<UserDTO, UserInfoViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.Name))
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryViewModel { Id = x.Category.Id, Name = x.Category.Name })))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender));

            CreateMap<UserDTO, UserManageViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.IsBlocked, opts => opts.MapFrom(src => src.IsBlocked))
                .ForMember(
                    dest => dest.Role,
                    opts => opts.MapFrom(src => new RoleViewModel { Id = src.RoleId, Name = src.Role.Name }))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()));

            CreateMap<UserDTO, UserPreviewViewModel>()
                .ForMember(
                    dest => dest.Username,
                    opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()));

            CreateMap<UserDTO, ProfileDTO>()
                .ForMember(
                    dest => dest.UserPhoto,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryViewModel { Id = x.Category.Id, Name = x.Category.Name })));

            CreateMap<ProfileDTO, ProfileViewModel>().ReverseMap();

            CreateMap<LoginViewModel, UserDTO>();
            CreateMap<UserViewModel, UserDTO>();

            #endregion

            #region EVENT MAPPING
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Owners, opt => opt.MapFrom(x => x.Owners.Select(z=>z.User)))
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryViewModel { Id = x.Category.Id, Name = x.Category.Name })))
                .ForMember(dest => dest.PhotoBytes, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.EventSchedule.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.EventSchedule.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => (src.EventSchedule != null)))
                .ForMember(dest => dest.PhotoId, opts => opts.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.Inventories, opt => opt.MapFrom(src =>
                        src.Inventories.Select(x => new InventoryDTO
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuring = new UnitOfMeasuringDTO
                            {
                                Id = x.UnitOfMeasuring.Id,
                                UnitName = x.UnitOfMeasuring.UnitName,
                                ShortName = x.UnitOfMeasuring.ShortName,
                            },
                        })));

            CreateMap<EventDTO, Event>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Owners, opt => opt.MapFrom(src => src.Owners.Select(x =>
                   new EventOwner
                   {
                       UserId = x.Id,
                       EventId = src.Id,
                   })))
                .ForMember(dest => dest.Visitors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                        src.Inventories.Select(x => new Inventory
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuringId = x.UnitOfMeasuring.Id,
                        })));

            CreateMap<EventDTO, EventPreviewViewModel>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.PhotoBytes.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.CountVisitor, opts => opts.MapFrom(src => src.Visitors.Where(x => x.UserStatusEvent == 0).Count()))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => new CountryViewModel
                {
                    Id = src.City.Country.Id,
                    Name = src.City.Country.Name,
                }))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => new CityViewModel
                {
                    Id = src.City.Id,
                    Name = src.City.Name,
                }))
                .ForMember(dest => dest.MaxParticipants, opts => opts.MapFrom(src => src.MaxParticipants))
                .ForMember(dest => dest.Owners, opts => opts.MapFrom(src => src.Owners.Select(x =>
                   new UserPreviewViewModel
                    {
                        Birthday = x.Birthday,
                        Id = x.Id,
                        PhotoUrl = x.Photo != null ? x.Photo.Thumb.ToRenderablePictureString() : null,
                        Username = x.Name ?? x.Email.Substring(0, x.Email.IndexOf("@", StringComparison.Ordinal)),
                    }
               )));

            CreateMap<EventDTO, EventViewModel>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.PhotoBytes.Img.ToRenderablePictureString()))
                .ForMember(dest => dest.Visitors, opts => opts.MapFrom(src => src.Visitors.Select(x =>
                    new UserPreviewViewModel
                    {
                        Id = x.User.Id,
                        Username = x.User.Name ?? x.User.Email.Substring(0, x.User.Email.IndexOf("@", StringComparison.Ordinal)),
                        Birthday = x.User.Birthday,
                        PhotoUrl = x.User.Photo != null ? x.User.Photo.Thumb.ToRenderablePictureString() : null,
                        UserStatusEvent = x.UserStatusEvent,
                    })))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => new CountryViewModel
                {
                    Id = src.City.Country.Id,
                    Name = src.City.Country.Name,
                }))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => new CityViewModel
                {
                    Id = src.City.Id,
                    Name = src.City.Name,
                }))
                .ForMember(dest => dest.Owners, opts => opts.MapFrom(src => src.Owners.Select(x =>
                 new UserPreviewViewModel
                 {
                     Id = x.Id,
                     Username = x.Name ?? x.Email.Substring(0, x.Email.IndexOf("@", StringComparison.Ordinal)),
                     Birthday = x.Birthday,
                     PhotoUrl = x.Photo != null ? x.Photo.Thumb.ToRenderablePictureString() : null,
                 })))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.MaxParticipants, opts => opts.MapFrom(src => src.MaxParticipants))
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                        src.Inventories.Select(x => new InventoryViewModel
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuring = new UnitOfMeasuringViewModel
                            {
                                Id = x.UnitOfMeasuring.Id,
                                UnitName = x.UnitOfMeasuring.UnitName,
                                ShortName = x.UnitOfMeasuring.ShortName,
                            },
                        })));

            CreateMap<EventEditViewModel, EventDTO>()
                .ForMember(dest => dest.OwnerIds, opts => opts.MapFrom(src => src.Owners.Select(x => new UserPreviewViewModel { Id = x.Id })));

            CreateMap<EventCreateViewModel, EventDTO>()
                .ForMember(dest => dest.OwnerIds, opts => opts.MapFrom(src => src.Owners.Select(x => new UserPreviewViewModel { Id = x.Id })))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                        src.Inventories.Select(x => new InventoryDTO
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuring = new UnitOfMeasuringDTO
                            {
                                UnitName = x.UnitOfMeasuring.UnitName,
                                ShortName = x.UnitOfMeasuring.ShortName,
                            },
                        })));

            CreateMap<EventViewModel, EventDTO>()
                .ForMember(dest => dest.CityId, opts => opts.MapFrom(src => src.City.Id))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.Inventories, opts => opts.MapFrom(src =>
                        src.Inventories.Select(x => new InventoryDTO
                        {
                            Id = x.Id,
                            ItemName = x.ItemName,
                            NeedQuantity = x.NeedQuantity,
                            UnitOfMeasuring = new UnitOfMeasuringDTO
                            {
                                Id = x.UnitOfMeasuring.Id,
                                UnitName = x.UnitOfMeasuring.UnitName,
                                ShortName = x.UnitOfMeasuring.ShortName,
                            },
                        })));

            #endregion

            #region CATEGORY MAPPING
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<CategoryViewModel, Category>();
            #endregion

            #region COMMENT MAPPING
            CreateMap<CommentDTO, Comments>().ReverseMap();
            CreateMap<CommentDTO, CommentViewModel>()
                .ForMember(
                    dest => dest.UserPhoto,
                    opts => opts.MapFrom(src => src.User.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(
                    dest => dest.UserName,
                    opts => opts.MapFrom(src => src.User.Name ?? src.User.Email.Substring(0, src.User.Email.IndexOf("@", StringComparison.Ordinal))));

            CreateMap<CommentViewModel, CommentDTO>();
            #endregion

            #region ROLE MAPPING
            CreateMap<Role, RoleViewModel>();

            #endregion

            #region ATTITUDE MAPPING
            CreateMap<AttitudeDTO, AttitudeViewModel>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => src.Attitude));

            CreateMap<AttitudeViewModel, AttitudeDTO>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => src.Attitude));

            CreateMap<AttitudeDTO, Relationship>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => (Attitude)src.Attitude));

            #endregion

            #region MESSAGE MAPPING

            CreateMap<ChatRoom, UserChatViewModel>()
                .ForMember(dest => dest.LastMessage, opts => opts.MapFrom(src => src.Messages.LastOrDefault().Text))
                .ForMember(dest => dest.LastMessageTime, opts => opts.MapFrom(src => src.Messages.LastOrDefault().DateCreated))
                .ForMember(dest => dest.Users, opts => opts.MapFrom(src => src.Users
                .Select(x => new UserPreviewViewModel
                {
                    Id = x.UserId,
                    Birthday = x.User.Birthday,
                    PhotoUrl = x.User.Photo != null ? x.User.Photo.Thumb.ToRenderablePictureString() : null,
                    Username = x.User.Name ?? x.User.Email.Substring(0, x.User.Email.IndexOf("@")),
                })));

            CreateMap<ChatRoom, ChatViewModel>()
                .ForMember(dest => dest.Messages, opts => opts.MapFrom(src => src.Messages.Select(x => new MessageViewModel
                {
                    Id = x.Id,
                    ChatRoomId = x.ChatRoomId,
                    DateCreated = x.DateCreated,
                    SenderId = x.SenderId,
                    Seen = x.Seen,
                    Edited = x.Edited,
                    Text = x.Text,
                })))
                .ForMember(dest => dest.Users, opts => opts.MapFrom(src => src.Users
                .Select(x => new UserPreviewViewModel
                {
                    Id = x.UserId,
                    Birthday = x.User.Birthday,
                    PhotoUrl = x.User.Photo != null ? x.User.Photo.Thumb.ToRenderablePictureString() : null,
                    Username = x.User.Name ?? x.User.Email.Substring(0, x.User.Email.IndexOf("@")),
                })));

            CreateMap<Message, MessageViewModel>().ReverseMap();

            #endregion

            #region REFRESHTOKEN MAPPING
            CreateMap<RefreshToken, RefreshTokenDTO>();
            CreateMap<RefreshTokenDTO, RefreshToken>();
            #endregion

            #region INVENTORY MAPPING
            CreateMap<Inventory, InventoryDTO>();
            CreateMap<InventoryDTO, Inventory>()
                .ForMember(dest => dest.UnitOfMeasuring, opt => opt.Ignore());
            CreateMap<InventoryDTO, InventoryViewModel>().ReverseMap();
            #endregion

            #region UNITOFMEASURING MAPPING
            CreateMap<UnitOfMeasuring, UnitOfMeasuringDTO>().ReverseMap();
            CreateMap<UnitOfMeasuringDTO, UnitOfMeasuringViewModel>().ReverseMap();
            #endregion
        }
    }
}
