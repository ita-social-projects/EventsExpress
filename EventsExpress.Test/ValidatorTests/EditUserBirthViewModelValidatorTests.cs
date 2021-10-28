namespace EventsExpress.Test.ValidatorTests
{
    using System;
    using EventsExpress.Validation;
    using EventsExpress.ViewModels;
    using FluentValidation;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    [TestFixture]
    internal class EditUserBirthViewModelValidatorTests
    {
        private IValidator<EditUserBirthViewModel> validator;
        private EditUserBirthViewModel birthDate;

        [SetUp]
        public void Setup()
        {
            validator = new EditUserBirthViewModelValidator();
            birthDate = new EditUserBirthViewModel();
        }

        [TestCase(2005, 12, 12)]
        [TestCase(1999, 11, 12)]
        [TestCase(1999, 7, 8)]
        public void SetBirthDate_ValidBirthDate_ValidationErrorIsNotReturn(int year, int month, int day)
        {
            // Arrange
            birthDate.Birthday = new DateTime(year, month, day);

            // Act
            var result = validator.TestValidate(birthDate);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.Birthday);
        }

        [TestCase(2222, 12, 12)]
        [TestCase(1800, 11, 12)]

        public void SetBirthDate_ValidBirthDate_ValidationErrorIsReturn(int year, int month, int day)
        {
            // Arrange
            birthDate.Birthday = new DateTime(year, month, day);

            // Act
            var result = validator.TestValidate(birthDate);

            // Assert
            result.ShouldHaveValidationErrorFor(e => e.Birthday);
        }
    }
}
