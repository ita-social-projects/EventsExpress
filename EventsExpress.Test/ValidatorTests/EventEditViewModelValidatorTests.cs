using System;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests
{
    [TestFixture]
    internal class EventEditViewModelValidatorTests
    {
        private EventEditViewModelValidator validator;
        private EventEditViewModel eventViewModel;
        private Mock<ICategoryService> mockCategoryService;

        [SetUp]
        public void Setup()
        {
            mockCategoryService = new Mock<ICategoryService>();
            validator = new EventEditViewModelValidator(mockCategoryService.Object);
            eventViewModel = new EventEditViewModel
            {
                Title = "Some title",
                Description = "Some desc",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                Latitude = 28.489335,
                Longitude = 56.498438,
                IsReccurent = true,
                Frequency = 1,
                Periodicity = Periodicity.Daily,
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

        [Test]
        public void SetCategoriesForEvent_ValidCategories_ValidationErrorIsNotReturn()
        {
            // Arrange
            Guid id = new Guid("20d71c34-f53a-4c8d-b043-7f3261e7b627");
            eventViewModel.Categories = new CategoryViewModel[] { new CategoryViewModel { Id = id } };
            mockCategoryService.Setup(service => service.Exists(id)).Returns(true);

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Categories);
        }

        public void SetCategoriesForEvent_EmptyCategoriesCollection_ReturnValidationError()
        {
            // Arrange
            eventViewModel.Categories = new CategoryViewModel[] { };

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Categories);
        }

        public void SetCategoriesForEvent_EmptyCategory_ReturnValidationError()
        {
            // Arrange
            eventViewModel.Categories = new CategoryViewModel[] { null };

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Categories);
        }

        public void SetCategoriesForEvent_NotExistingCategory_ReturnValidationError()
        {
            // Arrange
            Guid id = new Guid("20d71c34-f53a-4c8d-b043-7f3261e7b627");
            eventViewModel.Categories = new CategoryViewModel[] { new CategoryViewModel { Id = id } };
            mockCategoryService.Setup(service => service.Exists(id)).Returns(false);

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Categories);
        }

        [Test]
        public void SetFrequencyForEvent_ValidFrequency_ValidationErrorIsNotReturn()
        {
            // Arrange

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Frequency);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void SetFrequencyForEvent_InvalidFrequency_ReturnValidationError(int frequency)
        {
            // Arrange
            eventViewModel.Frequency = frequency;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Frequency);
        }

        [TestCase(Periodicity.Daily)]
        [TestCase(Periodicity.Weekly)]
        [TestCase(Periodicity.Monthly)]
        [TestCase(Periodicity.Weekly)]
        public void SetPeriodicityForEvent_ValidPeriodicity_ValidationErrorIsNotReturn(Periodicity periodicity)
        {
            // Arrange
            eventViewModel.Periodicity = periodicity;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Periodicity);
        }

        [TestCase(-1)]
        [TestCase(4)]
        public void SetPeriodicityForEvent_InvalidPeriodicity_ReturnValidationError(int periodicity)
        {
            // Arrange
            eventViewModel.Periodicity = (Periodicity)periodicity;

            // Act
            var result = validator.TestValidate(eventViewModel);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Periodicity);
        }
    }
}
