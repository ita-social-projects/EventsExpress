using System.Collections.Generic;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Helpers;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class RegisterMapperProfile : Profile
    {
        public RegisterMapperProfile()
        {
            CreateMap<LoginViewModel, RegisterDto>();
            CreateMap<CompleteRegistrationViewModel, CompleteRegistrationDto>();

            CreateMap<RegisterDto, Account>()
                .ForMember(dest => dest.AuthLocal, opts => opts.MapFrom(src => MapAuthLocal(src)));

            CreateMap<CompleteRegistrationDto, UserDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Username));
        }

        private static AuthLocal MapAuthLocal(RegisterDto src)
        {
            var salt = PasswordHasher.GenerateSalt();
            return new AuthLocal
            {
                Email = src.Email,
                Salt = salt,
                PasswordHash = PasswordHasher.GenerateHash(src.Password, salt),
            };
        }

    // .ForMember(dest => dest.AuthExternal, opts => opts.MapFrom(src => MapAuthExternal(src)))
    //    private static IEnumerable<AuthExternal> MapAuthExternal(RegisterDto src)
    //    {
    //        if (src.AuthType.HasValue)
    //        {
    //            return new[]
    //            {
    //                new AuthExternal
    //                {
    //                    Email = src.Email,
    //                    Type = src.AuthType.Value,
    //                },
    //            };
    //        }

    // return null;
    //    }
    }
}
