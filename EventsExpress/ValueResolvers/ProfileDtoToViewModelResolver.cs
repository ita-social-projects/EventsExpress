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
    public class ProfileDtoToViewModelResolver : IValueResolver<ProfileDto, ProfileViewModel, string>
    {
        private readonly IPhotoService photoService;

        public ProfileDtoToViewModelResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(ProfileDto source, ProfileViewModel destination, string destMember, ResolutionContext context) =>
            photoService.GetPhotoFromAzureBlob($"users/{source.Id}/photo.png").Result;
    }
}
