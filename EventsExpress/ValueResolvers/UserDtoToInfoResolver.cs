using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;

namespace EventsExpress.ValueResolvers
{
    public class UserDtoToInfoResolver : IValueResolver<UserDto, UserInfoViewModel, string>
    {
        private IPhotoService photoService;

        public UserDtoToInfoResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(UserDto userDto, UserInfoViewModel userInfoViewModel, string dest, ResolutionContext context) =>
            photoService.GetPhotoFromAzureBlob($"users/{userDto.Id}/photo.png").Result;
    }
}
