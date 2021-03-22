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
    public class EventDtoToViewModelResolver : IValueResolver<EventDto, EventViewModel, string>
    {
        private IPhotoService photoService;

        public EventDtoToViewModelResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(EventDto dto, EventViewModel viewModel, string dest, ResolutionContext context)
        {
            foreach (var u in viewModel.Owners)
            {
                u.PhotoUrl = photoService.GetPhotoFromAzureBlob($"users/{u.Id}/photo.png").Result;
            }

            foreach (var u in viewModel.Visitors)
            {
                u.PhotoUrl = photoService.GetPhotoFromAzureBlob($"users/{u.Id}/photo.png").Result;
            }

            return photoService.GetPhotoFromAzureBlob($"events/{dto.Id}/full.png").Result;
        }
    }
}
