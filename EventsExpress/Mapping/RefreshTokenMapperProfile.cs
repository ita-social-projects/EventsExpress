using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Mapping
{
    public class RefreshTokenMapperProfile : Profile
    {
        public RefreshTokenMapperProfile()
        {
            CreateMap<UserToken, RefreshTokenDto>();
            CreateMap<RefreshTokenDto, UserToken>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type))
                .ForMember(dest => dest.AccountId, opts => opts.Ignore())
                .ForMember(dest => dest.Account, opts => opts.Ignore());
        }
    }
}
