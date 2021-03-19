using System;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class CommentMapperProfile : Profile
    {
        private readonly IPhotoService photoService;

        public CommentMapperProfile(IPhotoService photoService)
        {
            CreateMap<CommentDto, Comments>()
                .ForMember(dest => dest.Event, opts => opts.Ignore())
                .ForMember(dest => dest.Parent, opts => opts.Ignore())
                .ReverseMap();

            CreateMap<CommentDto, CommentViewModel>()
                .ForMember(
                    dest => dest.UserName,
                    opts => opts.MapFrom(src => src.User.Name ?? src.User.Email.Substring(0, src.User.Email.IndexOf("@", StringComparison.Ordinal))))
                .AfterMap((src, dest) => dest.UserPhoto = photoService.GetPhotoFromAzureBlob($"users/{src.User.Id}/photo.png").Result);

            CreateMap<CommentViewModel, CommentDto>()
                .ForMember(dest => dest.User, opts => opts.Ignore());
            this.photoService = photoService;
        }
    }
}
