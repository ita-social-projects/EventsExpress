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
        private readonly IPhotoService photoService;

        public EventDtoToPreviewResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(EventDto source, EventPreviewViewModel destination, string destMember, ResolutionContext context)
        {
            foreach (var u in destination.Owners)
            {
                u.PhotoUrl = photoService.GetPhotoFromAzureBlob($"users/{u.Id}/photo.png").Result;
            }

            return photoService.GetPhotoFromAzureBlob($"events/{source.Id}/preview.png").Result;
        }
    }
}
