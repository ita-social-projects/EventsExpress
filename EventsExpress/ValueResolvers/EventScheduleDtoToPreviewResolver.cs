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
    public class EventScheduleDtoToPreviewResolver : IValueResolver<EventScheduleDto, PreviewEventScheduleViewModel, string>
    {
        private IPhotoService photoService;

        public EventScheduleDtoToPreviewResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public string Resolve(EventScheduleDto dto, PreviewEventScheduleViewModel viewModel, string dest, ResolutionContext context) =>
            photoService.GetPhotoFromAzureBlob($"events/{dto.Id}/preview.png").Result;
    }
}
