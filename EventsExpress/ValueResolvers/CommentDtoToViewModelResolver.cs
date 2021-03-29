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
    public class CommentDtoToViewModelResolver : IValueResolver<CommentDto, CommentViewModel, string>
    {
        private readonly IPhotoService photoService;

        public CommentDtoToViewModelResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(CommentDto source, CommentViewModel destination, string destMember, ResolutionContext context) =>
            photoService.GetPhotoFromAzureBlob($"users/{source.UserId}/photo.png").Result;
    }
}
