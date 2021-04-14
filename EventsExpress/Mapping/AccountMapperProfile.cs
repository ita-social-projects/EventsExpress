using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<AuthDto, AuthViewModel>();

            CreateMap<AuthExternal, AuthDto>()
                .ConvertUsing(ae => new AuthDto
                {
                    Email = ae.Email,
                    Type = ae.Type,
                });

            CreateMap<AuthLocal, AuthDto>()
                .ConvertUsing(al => new AuthDto
                {
                    Email = al.Email,
                    Type = null,
                });
        }
    }
}
