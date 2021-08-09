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
                .ForMember(dest => dest.Id, opts => opts.Ignore());
        }
    }
}
