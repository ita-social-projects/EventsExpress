using System;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.DTO;

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

            CreateMap<UserDTO, UserInfo>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.Name))
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name })))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender));

            CreateMap<UserDTO, UserManageDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.Name ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.IsBlocked, opts => opts.MapFrom(src => src.IsBlocked))
                .ForMember(
                    dest => dest.Role,
                    opts => opts.MapFrom(src => new RoleDto { Id = src.RoleId, Name = src.Role.Name }))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()));

            CreateMap<UserDTO, UserPreviewDto>()
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
                        src.Categories.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name })));

            CreateMap<ProfileDTO, ProfileDto>().ReverseMap();

            CreateMap<LoginDto, UserDTO>();
            CreateMap<UserView, UserDTO>();

            #endregion

            #region EVENT MAPPING
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Frequency, opt => opt.Ignore())
                .ForMember(
                    dest => dest.Categories,
                    opts => opts.MapFrom(src =>
                        src.Categories.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name })))
                .ForMember(dest => dest.PhotoBytes, opt => opt.MapFrom(src => src.Photo));

            CreateMap<EventDTO, Event>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Visitors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore());

            CreateMap<EventDTO, EventPreviewDto>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.PhotoBytes.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.CountVisitor, opts => opts.MapFrom(src => src.Visitors.Count()))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.City.Country.Name))
                .ForMember(dest => dest.CountryId, opts => opts.MapFrom(src => src.City.Country.Id))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.CityId, opts => opts.MapFrom(src => src.City.Id))
                .ForMember(dest => dest.MaxParticipants, opts => opts.MapFrom(src => src.MaxParticipants))
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => new UserPreviewDto
                {
                    Id = src.OwnerId,
                    Birthday = src.Owner.Birthday,
                    PhotoUrl = src.Owner.Photo != null ? src.Owner.Photo.Thumb.ToRenderablePictureString() : null,
                    Username = src.Owner.Name ?? src.Owner.Email.Substring(0, src.Owner.Email.IndexOf("@", StringComparison.Ordinal)),
                }));

            CreateMap<EventDTO, EventDto>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.PhotoBytes.Img.ToRenderablePictureString()))
                .ForMember(dest => dest.Visitors, opts => opts.MapFrom(src => src.Visitors.Select(x =>
                    new UserPreviewDto
                    {
                        Id = x.User.Id,
                        Username = x.User.Name ?? x.User.Email.Substring(0, x.User.Email.IndexOf("@", StringComparison.Ordinal)),
                        Birthday = x.User.Birthday,
                        PhotoUrl = x.User.Photo != null ? x.User.Photo.Thumb.ToRenderablePictureString() : null,
                    })))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.City.Country.Name))
                .ForMember(dest => dest.CountryId, opts => opts.MapFrom(src => src.City.Country.Id))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.CityId, opts => opts.MapFrom(src => src.City.Id))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.MaxParticipants, opts => opts.MapFrom(src => src.MaxParticipants))
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => new UserPreviewDto
                {
                    Id = src.OwnerId,
                    Birthday = src.Owner.Birthday,
                    PhotoUrl = src.Owner.Photo != null ? src.Owner.Photo.Thumb.ToRenderablePictureString() : null,
                    Username = src.Owner.Name ?? src.Owner.Email.Substring(0, src.Owner.Email.IndexOf("@", StringComparison.Ordinal)),
                }));

            CreateMap<EventDto, EventDTO>()
                .ForMember(dest => dest.CityId, opts => opts.MapFrom(src => src.CityId))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.IsReccurent, opts => opts.MapFrom(src => src.IsReccurent))
                .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.User.Id));

            #endregion

            #region OCCURENCE EVENT MAPPING

            CreateMap<OccurenceEvent, OccurenceEventDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.LastRun, opts => opts.MapFrom(src => src.LastRun))
                .ForMember(dest => dest.NextRun, opts => opts.MapFrom(src => src.NextRun))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.EventId, opts => opts.MapFrom(src => src.EventId));

            CreateMap<OccurenceEventDTO, OccurenceEvent>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.LastRun, opts => opts.MapFrom(src => src.LastRun))
                .ForMember(dest => dest.NextRun, opts => opts.MapFrom(src => src.NextRun))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.EventId, opts => opts.MapFrom(src => src.EventId));

            CreateMap<OccurenceEventDTO, OccurenceEventDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.LastRun, opts => opts.MapFrom(src => src.LastRun))
                .ForMember(dest => dest.NextRun, opts => opts.MapFrom(src => src.NextRun))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.EventId, opts => opts.MapFrom(src => src.EventId));

            CreateMap<OccurenceEventDto, OccurenceEventDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Periodicity, opts => opts.MapFrom(src => src.Periodicity))
                .ForMember(dest => dest.LastRun, opts => opts.MapFrom(src => src.LastRun))
                .ForMember(dest => dest.NextRun, opts => opts.MapFrom(src => src.NextRun))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.EventId, opts => opts.MapFrom(src => src.EventId));

            CreateMap<EventDTO, OccurenceEventDTO>()
                .ForMember(dest => dest.EventId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastRun, opts => opts.MapFrom(src => src.DateTo))
                .ForMember(dest => dest.NextRun, opts => opts.MapFrom(src => DateTimeExtensions.AddDateUnit(src.Periodicity, src.Frequency, src.DateTo)))
                .ForMember(dest => dest.Frequency, opts => opts.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.CreatedBy, opts => opts.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.CreatedDate, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ModifiedBy, opts => opts.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.ModifiedDate, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            #endregion

            #region CATEGORY MAPPING
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, CategoryDto>().ReverseMap();
            CreateMap<CategoryDto, Category>();
            #endregion

            #region COMMENT MAPPING
            CreateMap<CommentDTO, Comments>().ReverseMap();
            CreateMap<CommentDTO, CommentDto>()
                .ForMember(
                    dest => dest.UserPhoto,
                    opts => opts.MapFrom(src => src.User.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(
                    dest => dest.UserName,
                    opts => opts.MapFrom(src => src.User.Name ?? src.User.Email.Substring(0, src.User.Email.IndexOf("@", StringComparison.Ordinal))));

            CreateMap<CommentDto, CommentDTO>();
            #endregion

            #region ROLE MAPPING
            CreateMap<Role, RoleDto>();

            #endregion

            #region ATTITUDE MAPPING
            CreateMap<AttitudeDTO, AttitudeDto>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => src.Attitude));

            CreateMap<AttitudeDto, AttitudeDTO>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => src.Attitude));

            CreateMap<AttitudeDTO, Relationship>()
                .ForMember(dest => dest.Attitude, opts => opts.MapFrom(src => (Attitude)src.Attitude));

            #endregion

            #region MESSAGE MAPPING

            CreateMap<ChatRoom, UserChatDto>()
                .ForMember(dest => dest.LastMessage, opts => opts.MapFrom(src => src.Messages.LastOrDefault().Text))
                .ForMember(dest => dest.LastMessageTime, opts => opts.MapFrom(src => src.Messages.LastOrDefault().DateCreated))
                .ForMember(dest => dest.Users, opts => opts.MapFrom(src => src.Users
                .Select(x => new UserPreviewDto
                {
                    Id = x.UserId,
                    Birthday = x.User.Birthday,
                    PhotoUrl = x.User.Photo != null ? x.User.Photo.Thumb.ToRenderablePictureString() : null,
                    Username = x.User.Name ?? x.User.Email.Substring(0, x.User.Email.IndexOf("@")),
                })));

            CreateMap<ChatRoom, ChatDto>()
                .ForMember(dest => dest.Messages, opts => opts.MapFrom(src => src.Messages.Select(x => new MessageDto
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
                .Select(x => new UserPreviewDto
                {
                    Id = x.UserId,
                    Birthday = x.User.Birthday,
                    PhotoUrl = x.User.Photo != null ? x.User.Photo.Thumb.ToRenderablePictureString() : null,
                    Username = x.User.Name ?? x.User.Email.Substring(0, x.User.Email.IndexOf("@")),
                })));

            CreateMap<Message, MessageDto>().ReverseMap();

            #endregion

            #region REFRESHTOKEN MAPPING
            CreateMap<RefreshToken, RefreshTokenDTO>();
            CreateMap<RefreshTokenDTO, RefreshToken>();
            #endregion

        }
    }
}
