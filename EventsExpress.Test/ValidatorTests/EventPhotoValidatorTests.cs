namespace EventsExpress.Test.ValidatorTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using EventsExpress.Db.Entities;
    using EventsExpress.Test.ServiceTests.TestClasses.Photo;
    using EventsExpress.Validation;
    using EventsExpress.ViewModels;
    using FluentValidation;
    using FluentValidation.TestHelper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.StaticFiles;
    using NUnit.Framework;

    [TestFixture]
    internal class EventPhotoValidatorTests
    {
        private IValidator<EventPhotoViewModel> validator;
        private EventPhotoViewModel eventPhotoModel;

        [SetUp]
        public void Setup()
        {
            validator = new EventPhotoValidator();
            eventPhotoModel = new EventPhotoViewModel();
        }

        [Test]
        [TestCase(@"./Images/invalidFile.txt")]
        [TestCase(@"./Images/invalidFile.html")]
        [TestCase(@"./Images/tooSmallImage.jpg")]
        public void Photo_FalseValidation(string path)
        {
            using var stream = new MemoryStream();
            var file = PhotoHelpers.GetPhoto(path, stream);
            eventPhotoModel.Photo = file;

            var result = validator.TestValidate(eventPhotoModel);
            result.ShouldHaveAnyValidationError();
        }

        [Test]
        [TestCase(@"./Images/valid-event-image.jpg")]
        public void Photo_PassValidation(string path)
        {
            using var stream = new MemoryStream();
            var file = PhotoHelpers.GetPhoto(path, stream);
            eventPhotoModel.Photo = file;

            var result = validator.TestValidate(eventPhotoModel);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
