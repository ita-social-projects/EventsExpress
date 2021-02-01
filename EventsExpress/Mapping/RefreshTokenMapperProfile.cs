using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Mapping
{
    public class RefreshTokenMapperProfile : Profile
    {
        public RefreshTokenMapperProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<RefreshTokenDto, RefreshToken>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
        }
    }
}
