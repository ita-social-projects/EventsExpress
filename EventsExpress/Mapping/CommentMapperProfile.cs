using System;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<CommentDTO, Comments>()
                .ForMember(dest => dest.Event, opts => opts.Ignore())
                .ForMember(dest => dest.Parent, opts => opts.Ignore())
                .ReverseMap();

            CreateMap<CommentDTO, CommentViewModel>()
                .ForMember(
                    dest => dest.UserPhoto,
                    opts => opts.MapFrom(src => src.User.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(
                    dest => dest.UserName,
                    opts => opts.MapFrom(src => src.User.Name ?? src.User.Email.Substring(0, src.User.Email.IndexOf("@", StringComparison.Ordinal))));

            CreateMap<CommentViewModel, CommentDTO>()
                .ForMember(dest => dest.User, opts => opts.Ignore());
        }
    }
}
