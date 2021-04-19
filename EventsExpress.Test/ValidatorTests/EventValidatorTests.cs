using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Test.ValidationTests.TestClasses.Location;
using EventsExpress.Test.ValidatorTests.TestClasses.Location;
using EventsExpress.Validation;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests
{
    [TestFixture]
    internal class EventValidatorTests
    {
        private IValidator<Event> validator;
        private Event ev;

        [SetUp]
        public void Setup()
        {
            validator = new EventValidator();
            ev = new Event
            {
                Title = "Some title",
                Description = "Some desc",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
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
            ev.Title = title;

            // Act
            var result = validator.TestValidate(ev);

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
            ev.Title = title;

            // Act
            var result = validator.TestValidate(ev);

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
            ev.Description = desc;

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Description);
        }

        [TestCase("")]
        public void SetDescForEvent_InvalidDesc_ReturnValidationError(string desc)
        {
            // Arrange
            ev.Description = desc;

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Description);
        }

        [Test]
        public void SetDateFromForEvent_ValidDateFrom_ValidationErrorIsNotReturn()
        {
            // Arrange
            ev.DateFrom = DateTime.Now.AddDays(1);

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.DateFrom);
        }

        [TestCase(2020, 12, 12)]
        [TestCase(0, 0, 0)]
        public void SetDateFromForEvent_InvalidDateFrom_ReturnValidationError(int year, int month, int day)
        {
            // Arrange
            ev.DateFrom = year > 0 && month > 0 && year > 0 ?
            new DateTime(year, month, day) : default;

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.DateFrom);
        }

        [Test]
        public void SetDateToForEvent_ValidDateTo_ValidationErrorIsNotReturn()
        {
            // Arrange
            ev.DateTo = DateTime.Now.AddDays(1);

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.DateTo);
        }

        [Test]
        public void SetDateToForEvent_InvalidDateTo_ReturnValidationError()
        {
            // Arrange
            ev.DateTo = default;

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.DateTo);
        }

        [Test]
        public void SetMaxParticipantsForEvent_ValidMaxParticipants_ValidationErrorIsNotReturn()
        {
            // Arrange

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.MaxParticipants);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void SetMaxParticipantsForEvent_InvalidMaxParticipants_ReturnValidationError(int maxParticipants)
        {
            // Arrange
            ev.MaxParticipants = maxParticipants;

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.MaxParticipants);
        }

        [TestCaseSource(typeof(CorrectLocation))]
        public void Check_CorrectLocation_validation(EventLocation location)
        {
            // Arrange
            ev.EventLocation = location;

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.EventLocation);
        }
<<<<<<< HEAD

        [Test]
        public void Check_CorrectPhotoId_validation()
        {
            // Arrange

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.PhotoId);
        }

        [Test]
        public void Check_CorrectPhoto_validation()
        {
            // Arrange

            // Act
            var result = validator.TestValidate(ev);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Photo);
        }
=======
>>>>>>> origin/development
    }
}
