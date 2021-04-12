using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<AuthDto, AuthViewModel>();
        }
    }
}
