namespace EventsExpress.Validation
{
    using System.IO;
    using System.Linq;
    using EventsExpress.Core.Extensions;
    using EventsExpress.ViewModels;
    using FluentValidation;
    using FreeImageAPI;
    using Microsoft.AspNetCore.Http;

    public abstract class BasePhotoValidator<T> : AbstractValidator<T>
        where T : PhotoViewModelBase
    {
        private readonly string[] allowedExtensions = { "jpeg", "jpg", "png", "bmp" };

        public BasePhotoValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(f => f.Photo).NotEmpty().Must(ValidImage).OverridePropertyName("image")
                .WithMessage("The upload file should be a valid image!");
            RuleFor(f => f.Photo).Must(f => !allowedExtensions.Contains(Path.GetExtension(f.FileName))).OverridePropertyName("image")
                .WithMessage("Accepted file formats are .jpeg, .jpg, .png, or .bmp");
            RuleFor(f => f.Photo).Must(f => f.Length < MaxFileSize).OverridePropertyName("image")
                .WithMessage($"File size can not exceed {MaxFileSize} MB");
        }

        protected abstract int MaxFileSize { get; set; }

        private bool ValidImage(IFormFile file) => file.IsImage();
    }
}
