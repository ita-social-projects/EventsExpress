using System;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.ValueResolvers;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<CommentDto, Comments>()
                .ForMember(dest => dest.Event, opts => opts.Ignore())
                .ForMember(dest => dest.Parent, opts => opts.Ignore())
                .ReverseMap();

            CreateMap<CommentDto, CommentViewModel>()
                .ForMember(
                    dest => dest.UserName,
                    opts => opts.MapFrom(src => src.User.Name ?? src.User.Email.Substring(0, src.User.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.UserPhoto, opts => opts.MapFrom<CommentDtoToViewModelResolver>());

            CreateMap<CommentViewModel, CommentDto>()
                .ForMember(dest => dest.User, opts => opts.Ignore());
        }
    }
}
