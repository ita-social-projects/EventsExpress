﻿using System.Collections.Generic;
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
            CreateMap<RegisterCompleteViewModel, RegisterCompleteDto>();

            CreateMap<RegisterDto, Account>()
                .ForMember(dest => dest.AuthLocal, opts => opts.MapFrom(src => MapAuthLocal(src)));

            CreateMap<RegisterCompleteDto, UserDto>()
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
    }
}