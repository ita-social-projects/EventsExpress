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
    public class UserDtoToManageResolver : IValueResolver<UserDto, UserManageViewModel, string>
    {
        private IPhotoService photoService;

        public UserDtoToManageResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(UserDto userDto, UserManageViewModel userManageViewModel, string dest, ResolutionContext context) =>
            photoService.GetPhotoFromAzureBlob($"users/{userDto.Id}/photo.png").Result;
    }
}
