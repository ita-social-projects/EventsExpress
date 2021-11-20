namespace EventsExpress.Test.ValidatorTests
{
    using System;
    using EventsExpress.Db.Entities;
    using EventsExpress.Validation;
    using EventsExpress.ViewModels;
    using FluentValidation;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    [TestFixture]
    internal class UserMoreInfoCreateViewModelValidatorTests
    {
        private IValidator<UserMoreInfoCreateViewModel> validator;
        private UserMoreInfoCreateViewModel userMoreInfo;

        [SetUp]
        public void Setup()
        {
            validator = new UserMoreInfoCreateViewModelValidator();
            userMoreInfo = new UserMoreInfoCreateViewModel
            {
                UserId = new Guid("e618879f-966e-4bbb-b8d2-c497586e4405"),
            };
        }

        [Test]
        public void SetTitleForEvent_ValidTitle_ValidationErrorIsNotReturn()
        {
            userMoreInfo.UserId = new Guid("e618879f-966e-4bbb-b8d2-c497586e4446");

            var result = validator.TestValidate(userMoreInfo);

            // Assert
            result.ShouldNotHaveValidationErrorFor(e => e.UserId);
        }
    }
}
