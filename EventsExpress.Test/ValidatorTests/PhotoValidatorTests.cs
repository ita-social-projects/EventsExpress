namespace EventsExpress.Test.ValidatorTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using EventsExpress.Db.Entities;
    using EventsExpress.Validation;
    using EventsExpress.ViewModels;
    using FluentValidation;
    using FluentValidation.TestHelper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.StaticFiles;
    using NUnit.Framework;

    [TestFixture]
    internal class PhotoValidatorTests
    {
        private IValidator<PhotoViewModel> validator;
        private PhotoViewModel photoModel;

        [SetUp]
        public void Setup()
        {
            validator = new PhotoValidator();
            photoModel = new PhotoViewModel();
        }

        [Test]
        [TestCase(@"./Images/invalidFile.txt")]
        [TestCase(@"./Images/invalidFile.html")]
        [TestCase(@"./Images/tooSmallImage.jpg")]
        public void Photo_FalseValidation(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(path);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            IFormFile file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            photoModel.Photo = file;

            var result = validator.TestValidate(photoModel);
            result.ShouldHaveAnyValidationError();
        }

        [Test]
        [TestCase(@"./Images/valid-image.jpg")]
        public void Photo_PassValidation(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(path);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            IFormFile file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            photoModel.Photo = file;

            var result = validator.TestValidate(photoModel);
            result.ShouldNotHaveAnyValidationErrors();
        }

        public string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
