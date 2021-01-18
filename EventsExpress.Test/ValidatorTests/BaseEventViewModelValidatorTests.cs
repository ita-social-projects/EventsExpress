using System;
using System.Collections.Generic;
using System.Text;
using EventsExpress.Validation.Base;
using EventsExpress.ViewModels.Base;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests
{
    [TestFixture]
    internal class BaseEventViewModelValidatorTests
    {
        private BaseEventViewModelValidator<EventViewModelBase> validator;
        private EventViewModelBase eventViewModel;

        [SetUp]
        public void Setup()
        {
            validator = new BaseEventViewModelValidator<EventViewModelBase>();
            eventViewModel = new EventViewModelBase
            {
                Title = "Some title",
                Description = "Some desc",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                Latitude = 28.489335,
                Longitude = 56.498438,
                MaxParticipants = 20,
            };
        }

        [TestCase("H")]
        [TestCase("HelloWorld!")]
        [TestCase("HelloWorldHelloWorldHelloWorld" +
                  "HelloWorldHelloWorldHelloWorld")]
        public void SetTitleForEvent_ValidTitle_ValidationErrorIsNotReturn(string title)
        {
            // Arrange
            eventViewModel.Title = title;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Title);
        }

        [TestCase(
            "",
            TestName = "SetTitleForEvent_EmptyTitle_ReturnValidationError")]
        [TestCase(
            "HelloWorldHelloWorldHelloWorldHelloWorldHelloWorldHelloWorld!",
            TestName = "SetTitleForEvent_Title_>_60_Characters_ReturnValidationError")]
        public void SetTitleForEvent_InvalidTitle_ReturnValidationError(string title)
        {
            // Arrange
            eventViewModel.Title = title;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Title);
        }

        [TestCase("H")]
        [TestCase("HelloWorld!")]
        [TestCase("HelloWorldHelloWorldHelloWorld" +
                  "HelloWorldHelloWorldHelloWorld!")]
        public void SetDescForEvent_ValidDesc_ValidationErrorIsNotReturn(string desc)
        {
            // Arrange
            eventViewModel.Description = desc;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Description);
        }

        [TestCase("")]
        public void SetDescForEvent_InvalidDesc_ReturnValidationError(string desc)
        {
            // Arrange
            eventViewModel.Description = desc;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Description);
        }

        [Test]
        public void SetDateFromForEvent_ValidDateFrom_ValidationErrorIsNotReturn()
        {
            // Arrange
            eventViewModel.DateFrom = DateTime.Now.AddDays(1);

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.DateFrom);
        }

        [TestCase(2020, 12, 12)]
        [TestCase(0, 0, 0)]
        public void SetDateFromForEvent_InvalidDateFrom_ReturnValidationError(int year, int month, int day)
        {
            // Arrange
            eventViewModel.DateFrom = year > 0 && month > 0 && year > 0 ?
            new DateTime(year, month, day) : default;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.DateFrom);
        }

        [Test]
        public void SetDateToForEvent_ValidDateTo_ValidationErrorIsNotReturn()
        {
            // Arrange
            eventViewModel.DateTo = DateTime.Now.AddDays(1);

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.DateTo);
        }

        [Test]
        public void SetDateToForEvent_InvalidDateTo_ReturnValidationError()
        {
            // Arrange
            eventViewModel.DateTo = default;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.DateTo);
        }

        [Test]
        public void SetLatitudeForEvent_ValidLatitude_ValidationErrorIsNotReturn()
        {
            // Arrange

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Latitude);
        }

        [Test]
        public void SetLatitudeForEvent_InvalidLatitude_ReturnValidationError()
        {
            // Arrange
            eventViewModel.Latitude = default;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Latitude);
        }

        [Test]
        public void SetLongitudeForEvent_ValidLongitude_ValidationErrorIsNotReturn()
        {
            // Arrange

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Longitude);
        }

        [Test]
        public void SetLongitudeForEvent_InvalidLongitude_ReturnValidationError()
        {
            // Arrange
            eventViewModel.Longitude = default;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Longitude);
        }

        [Test]
        public void SetMaxParticipantsForEvent_ValidMaxParticipants_ValidationErrorIsNotReturn()
        {
            // Arrange

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.MaxParticipants);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void SetMaxParticipantsForEvent_InvalidMaxParticipants_ReturnValidationError(int maxParticipants)
        {
            // Arrange
            eventViewModel.MaxParticipants = maxParticipants;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.MaxParticipants);
        }
    }
}
