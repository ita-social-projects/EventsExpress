using EventsExpress.Core.Extensions;
using EventsExpress.ViewModels;
using FluentValidation;
using FreeImageAPI;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Validation
{
    public class EventPhotoValidator : BasePhotoValidator<EventPhotoViewModel>
    {
        public EventPhotoValidator()
        {
            RuleFor(f => f.Photo).Must(ValidImageSize).OverridePropertyName("image")
             .WithMessage("Image size should be at least 400x400px");
        }

        protected override int MaxFileSize { get; set; } = 10 * 1024 * 1024;

        private bool ValidImageSize(IFormFile file)
        {
            using var memoryStream = file.ToMemoryStream();
            var image = new FreeImageBitmap(memoryStream);
            return image.Width >= 400 && image.Height >= 400;
        }
    }
}
