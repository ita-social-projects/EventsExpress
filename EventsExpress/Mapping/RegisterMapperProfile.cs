using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ValueResolvers;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping;

public class RegisterMapperProfile : Profile
{
    public RegisterMapperProfile()
    {
        CreateMap<LoginViewModel, RegisterDto>();

        CreateMap<RegisterCompleteViewModel, RegisterCompleteDto>()
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName));

        CreateMap<RegisterDto, Account>()
            .ForMember(dest => dest.AuthLocal, opts => opts.MapFrom<RegisterDtoToAccountResolver>());

        CreateMap<RegisterCompleteDto, UserDto>()
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName));
    }
}
