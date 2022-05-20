using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsExpress.Test.ServiceTests.TestClasses.Photo;
using EventsExpress.Validation;
using EventsExpress.ViewModels;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EventsExpress.Test.ValidatorTests
{
    [TestFixture]
    internal class UserPhotoValidatorTests
    {
        private IValidator<UserPhotoViewModel> validator;
        private UserPhotoViewModel userPhotoModel;

        [SetUp]
        public void Setup()
        {
            validator = new UserPhotoValidator();
            userPhotoModel = new UserPhotoViewModel();
        }

        [Test]
        [TestCase(@"./Images/invalidFile.txt")]
        [TestCase(@"./Images/invalidFile.html")]
        [TestCase(@"./Images/tooSmallImage.jpg")]
        [TestCase(@"./Images/valid-event-image.jpg")]
        public void Photo_FalseValidation(string path)
        {
            using var stream = new MemoryStream();
            var file = PhotoHelpers.GetPhoto(path, stream);
            userPhotoModel.Photo = file;

            var result = validator.TestValidate(userPhotoModel);
            result.ShouldHaveAnyValidationError();
        }

        [Test]
        [TestCase(@"./Images/valid-user-image.jpg")]
        public void Photo_PassValidation(string path)
        {
            using var stream = new MemoryStream();
            var file = PhotoHelpers.GetPhoto(path, stream);
            userPhotoModel.Photo = file;

            var result = validator.TestValidate(userPhotoModel);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
