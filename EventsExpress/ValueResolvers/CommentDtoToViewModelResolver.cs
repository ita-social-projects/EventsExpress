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
        private IPhotoService photoService;

        public CommentDtoToViewModelResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(CommentDto dto, CommentViewModel viewModel, string dest, ResolutionContext context) =>
            photoService.GetPhotoFromAzureBlob($"users/{dto.UserId}/photo.png").Result;
    }
}
