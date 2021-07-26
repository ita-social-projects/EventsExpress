namespace EventsExpress.Validation
{
    using EventsExpress.Core.Extensions;
    using EventsExpress.ViewModels;
    using FluentValidation;
    using Microsoft.AspNetCore.Http;

    public class PhotoValidator : AbstractValidator<PhotoViewModel>
    {
        public PhotoValidator()
        {
            RuleFor(f => f.Photo).NotEmpty().Must(ValidImage).WithMessage("The upload file should be a valid image!");
        }

        private bool ValidImage(IFormFile file) => file.IsImage();
    }
}
