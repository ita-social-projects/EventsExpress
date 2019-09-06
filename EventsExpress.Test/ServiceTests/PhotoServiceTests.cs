using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.StaticFiles;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using EventsExpress.Core.Infrastructure;
using Microsoft.Extensions.Options;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    class PhotoServiceTests : TestInitializer
    {
        private PhotoService _photoService;


        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            var mockOpt = new Mock<IOptions<ImageOptionsModel>>();
            mockOpt.Setup(opt => opt.Value.Thumbnail).Returns(400);
            mockOpt.Setup(opt => opt.Value.Image).Returns(1200);

            mockUnitOfWork.Setup(u => u.PhotoRepository.Insert(It.IsAny<Photo>()));

            _photoService = new PhotoService(mockUnitOfWork.Object, mockOpt.Object);
        }


        


        [Test]
        public void AddPhoto_ValidFormFile_DoesNotThrows()
        {
            using (var stream = File.OpenRead(@"./Images/valid-image.jpg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"./Images/valid-image.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = GetContentType(Path.GetFileName(@"./Images/valid-image.jpg"))
                };

                Assert.DoesNotThrowAsync(async () => await _photoService.AddPhoto(file));
            }
        }


        [Test]
        [TestCase(@"./Images/invalidFile.txt")]
        [TestCase(@"./Images/invalidFile.html")]
        [TestCase(@"./Images/tooSmallImage.jpg")]
        public void AddPhoto_InValidFormFile_WillThrows(string testFilePath)
        {
            string fileName = Path.GetFileName(testFilePath);

            using (var stream = File.OpenRead(testFilePath))
            {
                var file = new FormFile(stream, 0, stream.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = GetContentType(fileName)
                };
                Assert.ThrowsAsync<ArgumentException>(async () => await _photoService.AddPhoto(file));
            }
        }




        private string GetContentType(string fileName)
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
