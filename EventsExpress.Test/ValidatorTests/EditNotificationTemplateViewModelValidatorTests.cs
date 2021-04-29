using EventsExpress.Db.Enums;
using EventsExpress.Validation;
using EventsExpress.ViewModels;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests
{
    [TestFixture]
    internal class EditNotificationTemplateViewModelValidatorTests
    {
        private EditNotificationTemplateViewModelValidator _validator;
        private EditNotificationTemplateViewModel _templateViewModel;

        [SetUp]
        public void Setup()
        {
            _validator = new EditNotificationTemplateViewModelValidator();
            _templateViewModel = new EditNotificationTemplateViewModel
            {
                Id = NotificationProfile.BlockedUser,
                Subject = "testSubject",
                Message = "Sample text for message",
            };
        }

        [TestCase("")]
        [TestCase("Er")]
        [TestCase("Error")]
        public void Subject_ValidationErrors(string subject)
        {
            // Arrange
            _templateViewModel.Subject = subject;

            // Act
            var result = _validator.TestValidate(_templateViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Subject);
        }

        [TestCase("10 letters")]
        [TestCase("More than 10 letters")]
        public void Subject_IsValid(string subject)
        {
            // Arrange
            _templateViewModel.Subject = subject;

            // Act
            var result = _validator.TestValidate(_templateViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Subject);
        }

        [TestCase("Less")]
        [TestCase("Less than")]
        [TestCase("Less than 15 l")]
        public void Message_ValidationErrors(string message)
        {
            // Arrange
            _templateViewModel.Message = message;

            // Act
            var result = _validator.TestValidate(_templateViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Message);
        }

        [TestCase("Is 15 symbols!!")]
        [TestCase("More than 15 symbols")]
        public void Message_IsValid(string message)
        {
            // Arrange
            _templateViewModel.Message = message;

            // Act
            var result = _validator.TestValidate(_templateViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Message);
        }
    }
}
