namespace EventsExpress.Validation
{
    using EventsExpress.Core.Extensions;
    using FluentValidation;
    using Microsoft.AspNetCore.Http;

    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator()
        {
            RuleFor(f => f).NotEmpty().Must(ValidImage).WithMessage("The upload file should be a valid image!");
        }

        private bool ValidImage(IFormFile file) => file.IsImage();
    }
}
