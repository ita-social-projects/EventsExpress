using System;
using EventsExpress.Db.Enums;
using EventsExpress.Validation;
using EventsExpress.ViewModels;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EventsExpress.Test.ValidationTests
{
    [TestFixture]
    internal class RegisterCompleteViewModelValidatorTests
    {
        private IValidator<RegisterCompleteViewModel> validator;
        private RegisterCompleteViewModel model;

        [SetUp]
        public void Setup()
        {
            validator = new RegisterCompleteViewModelValidator();
            model = new RegisterCompleteViewModel
            {
                Birthday = new DateTime(2000, 1, 1),
                Email = "correct@mail.com",
                Gender = Gender.Male,
                Phone = "+380123456789",
                FirstName = "Username",
            };
        }

        [Test]
        [Category("Birthday validation")]
        public void ShoudNotHaveError_BirthdayIsCorrect()
        {
            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.Birthday);
        }

        [Test]
        [Category("Birthday validation")]
        public void ShouldHaveError_BirthdayIsDefaultValue()
        {
            model.Birthday = default;

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Birthday).WithErrorMessage("Birthday date is required");
        }

        [Test]
        [Category("Birthday validation")]
        public void ShouldHaveError_BirthdayIsOutOfRange()
        {
            model.Birthday = DateTime.Now;

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Birthday).WithErrorMessage("Birthday date is not correct");
        }

        [Test]
        [Category("Email validation")]
        public void ShouldNotHaveError_EmailIsCorrect()
        {
            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.Email);
        }

        [Test]
        [Category("Email validation")]
        public void ShouldHaveError_EmailIsDefault()
        {
            model.Email = default;

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Email).WithErrorMessage("Email address is required");
        }

        [Test]
        [Category("Email validation")]
        public void ShouldHaveError_EmailIsNotAnEmailAddress()
        {
            model.Email = "IsNotAnEmailAddress";

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Email).WithErrorMessage("Email address is not correct");
        }

        [Test]
        [Category("Gender validation")]
        public void ShouldNotHaveError_GenderIsCorrect()
        {
            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.Gender);
        }

        [Test]
        [Category("Gender validation")]
        public void ShouldHaveError_GenderIsDefaultDefault()
        {
            model.Gender = default;

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Gender).WithErrorMessage("Gender is required");
        }

        [Test]
        [Category("Gender validation")]
        public void ShouldHaveError_GenderIsOutOfEnum()
        {
            model.Gender = (Gender)4;

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Gender).WithErrorMessage("Gender is not correct");
        }

        [Test]
        [Category("Phone validation")]
        public void ShouldNotHaveError_PhoneIsCorrect()
        {
            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.Phone);
        }

        [Test]
        [Category("Phone validation")]
        public void ShouldHaveError_PhoneIsEmpty()
        {
            model.Phone = string.Empty;

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Phone).WithErrorMessage("Phone number is required");
        }

        [Test]
        [Category("Phone validation")]
        public void ShouldHaveError_PhoneIsNotAPhonenumber()
        {
            model.Phone = "IsNotAPhonenumber";

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Phone).WithErrorMessage("Phone number is not correct");
        }

        [Test]
        [Category("Username validation")]
        public void ShouldNotHaveError_UsernameIsCorrect()
        {
            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.FirstName);
        }

        [Test]
        [Category("Username validation")]
        public void ShouldHaveError_UsernameIsDefault()
        {
            model.FirstName = default;

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.FirstName).WithErrorMessage("First name is required");
        }

        [Test]
        [Category("Username validation")]
        public void ShouldHaveError_UsernameIsTooShort()
        {
            model.FirstName = "TS";

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.FirstName).WithErrorMessage("First name is too short");
        }

        [Test]
        [Category("Username validation")]
        public void ShouldHaveError_UsernameIsTooLong()
        {
            model.FirstName = "ThereAreTooLongUsernameSoLongThatValidationWillFailed";

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.FirstName).WithErrorMessage("First name is too long");
        }
    }
}
