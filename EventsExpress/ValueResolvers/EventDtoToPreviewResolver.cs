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
    public class EventDtoToPreviewResolver : IValueResolver<EventDto, EventPreviewViewModel, string>
    {
        private IPhotoService photoService;

        public EventDtoToPreviewResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(EventDto dto, EventPreviewViewModel viewModel, string dest, ResolutionContext context)
        {
            foreach (var u in viewModel.Owners)
            {
                u.PhotoUrl = photoService.GetPhotoFromAzureBlob($"users/{u.Id}/photo.png").Result;
            }

            return photoService.GetPhotoFromAzureBlob($"events/{dto.Id}/preview.png").Result;
        }
    }
}
