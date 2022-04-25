using EventsExpress.Core.Extensions;
using EventsExpress.ViewModels;
using FluentValidation;
using FreeImageAPI;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Validation
{
    public class UserPhotoValidator : BasePhotoValidator<UserPhotoViewModel>
    {
        public UserPhotoValidator()
        {
            RuleFor(f => f.Photo).Must(ValidImageSize).OverridePropertyName("image")
              .WithMessage("Image size should be at 110x110px");
        }

        protected override int MaxFileSize { get; set; } = 3 * 1024 * 1024;

        private bool ValidImageSize(IFormFile file)
        {
            using var memoryStream = file.ToMemoryStream();
            var image = new FreeImageBitmap(memoryStream);
            return image.Width == 110 && image.Height == 110;
        }
    }
}
