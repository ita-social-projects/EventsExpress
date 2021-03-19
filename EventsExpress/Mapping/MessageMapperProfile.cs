using System;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class MessageMapperProfile : Profile
    {
        private readonly IPhotoService photoService;

        public MessageMapperProfile(IPhotoService photoService)
        {
            CreateMap<ChatRoom, UserChatViewModel>()
                .ForMember(dest => dest.LastMessage, opts => opts.MapFrom(src => src.Messages.LastOrDefault().Text))
                .ForMember(dest => dest.LastMessageTime, opts => opts.MapFrom(src => src.Messages.LastOrDefault().DateCreated))
                .ForMember(dest => dest.Users, opts => opts.MapFrom(src => src.Users
                .Select(x => new UserPreviewViewModel
                {
                    Id = x.UserId,
                    Birthday = x.User.Birthday,
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
                    PhotoUrl = photoService.GetPhotoFromAzureBlob($"users/{x.UserId}/photo.png").Result != null ?
                        photoService.GetPhotoFromAzureBlob($"users/{x.UserId}/photo.png").Result : null,
                    Username = x.User.Name ?? x.User.Email.Substring(0, x.User.Email.IndexOf("@")),
                })))
                .ForMember(dest => dest.LastMessage, opts => opts.Ignore())
                .ForMember(dest => dest.LastMessageTime, opts => opts.Ignore());

            CreateMap<Message, MessageViewModel>().ReverseMap();
            this.photoService = photoService;
        }
    }
}
