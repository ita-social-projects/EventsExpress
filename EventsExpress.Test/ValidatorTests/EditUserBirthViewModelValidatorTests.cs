using System;
using EventsExpress.Validation;
using EventsExpress.ViewModels;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests
{
    [TestFixture]
    internal class EditUserBirthViewModelValidatorTests
    {
        private IValidator<EditUserBirthViewModel> validator;
        private EditUserBirthViewModel birthDate;

        [SetUp]
        protected void Initialize()
        {
            validator = new EditUserBirthViewModelValidator();
            birthDate = new EditUserBirthViewModel();
        }

        [Test]
        [TestCase(2005, 12, 12)]
        [TestCase(1999, 11, 12)]
        [TestCase(1999, 7, 8)]
        public void Validator_ValidBirthDate_ReturnsNoErrors(int year, int month, int day)
        {
            birthDate.Birthday = new DateTime(year, month, day);

            var result = validator.TestValidate(birthDate);

            result.ShouldNotHaveValidationErrorFor(e => e.Birthday);
        }

        [Test]
        [TestCase(2222, 12, 12)]
        [TestCase(2020, 1, 1)]
        [TestCase(1800, 11, 12)]
        public void Validator_InvalidBirthDate_ReturnsValidationError(int year, int month, int day)
        {
            birthDate.Birthday = new DateTime(year, month, day);

            var result = validator.TestValidate(birthDate);

            result.ShouldHaveValidationErrorFor(e => e.Birthday);
        }

        [Test]
        public void Validator_TimeSpecified_ReturnsValidationError()
        {
            birthDate.Birthday = new DateTime(2000, 1, 1, 12, 30, 0);

            var result = validator.TestValidate(birthDate);

            result.ShouldHaveValidationErrorFor(e => e.Birthday);
        }
    }
}
