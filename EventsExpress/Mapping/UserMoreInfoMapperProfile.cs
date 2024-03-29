﻿using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping;

public class UserMoreInfoMapperProfile : Profile
{
    public UserMoreInfoMapperProfile()
    {
        CreateMap<UserMoreInfoCreateViewModel, UserMoreInfoDto>()
            .ForMember(dest => dest.ParentStatus, opts => opts.MapFrom(src => src.ParentStatus))
            .ForMember(dest => dest.ReasonsForUsingTheSite, opts => opts.MapFrom(src => src.ReasonsForUsingTheSite))
            .ForMember(dest => dest.EventTypes, opts => opts.MapFrom(src => src.EventTypes))
            .ForMember(dest => dest.RelationShipStatus, opts => opts.MapFrom(src => src.RelationshipStatus))
            .ForMember(dest => dest.TheTypeOfLeisure, opts => opts.MapFrom(src => src.LeisureType))
            .ForMember(dest => dest.AdditionalInfo, opts => opts.MapFrom(src => src.AdditionalInfo))
            .ForAllOtherMembers(dest => dest.Ignore());

        CreateMap<UserMoreInfoDto, UserMoreInfo>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opts => opts.Ignore())
            .ForMember(dest => dest.ParentStatus, opts => opts.MapFrom(src => src.ParentStatus))
            .ForMember(dest => dest.ReasonsForUsingTheSite, opts => opts.MapFrom(src => (InterestReasons)src.ReasonsForUsingTheSite.Sum(et => (int)et)))
            .ForMember(dest => dest.EventTypes, opts => opts.MapFrom(src => (EventTypes)src.EventTypes.Sum(et => (int)et)))
            .ForMember(dest => dest.RelationShipStatus, opts => opts.MapFrom(src => src.RelationShipStatus))
            .ForMember(dest => dest.TheTypeOfLeisure, opts => opts.MapFrom(src => src.TheTypeOfLeisure))
            .ForMember(dest => dest.AdditionalInfo, opts => opts.MapFrom(src => src.AdditionalInfo));
    }
}
