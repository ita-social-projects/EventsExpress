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
    public class EventScheduleDtoToViewModelResolver : IValueResolver<EventScheduleDto, EventScheduleViewModel, string>
    {
        private IPhotoService photoService;

        public EventScheduleDtoToViewModelResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(EventScheduleDto dto, EventScheduleViewModel viewModel, string dest, ResolutionContext context)
        {
            foreach (var u in viewModel.Owners)
            {
                u.PhotoUrl = photoService.GetPhotoFromAzureBlob($"users/{u.Id}/photo.png").Result;
            }

            return photoService.GetPhotoFromAzureBlob($"events/{dto.Id}/full.png").Result;
        }
    }
}
