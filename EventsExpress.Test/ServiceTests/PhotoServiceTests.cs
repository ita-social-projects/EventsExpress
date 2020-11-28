using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class PhotoServiceTests : TestInitializer
    {
        private Guid photoId = Guid.NewGuid();

        public PhotoService PhotoService { get; set; }

        private Photo Photo { get; set; }

        private Mock<HttpMessageHandler> HttpMessageHandlerMock { get; set; }

        private Mock<IHttpClientFactory> HttpClientFactoryMock { get; set; }

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            var mockOpt = new Mock<IOptions<ImageOptionsModel>>();
            mockOpt.Setup(opt => opt.Value.Thumbnail).Returns(400);
            mockOpt.Setup(opt => opt.Value.Image).Returns(1200);

            HttpClientFactoryMock = new Mock<IHttpClientFactory>();
            HttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            HttpClientFactoryMock.Setup(f => f.CreateClient(string.Empty))
                .Returns(new HttpClient((HttpMessageHandler)HttpMessageHandlerMock.Object))
                .Verifiable();

            PhotoService = new PhotoService(
                Context, mockOpt.Object, HttpClientFactoryMock.Object);

            Photo = new Photo
            {
                Id = photoId,
                Thumb = new byte[0],
                Img = new byte[0],
            };

            Context.Photos.Add(Photo);
            Context.SaveChanges();
        }

        private void SetUpHttpHandlerMock(HttpStatusCode statusCode)
        {
            HttpMessageHandlerMock
                   .Protected()
                   .Setup<Task<HttpResponseMessage>>(
                         "SendAsync",
                         ItExpr.IsAny<HttpRequestMessage>(),
                         ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = statusCode,
                       Content = new StringContent($"{{\"expires_in\": 100, \"access_token\":\"\"}}"),
                   })

                   .Verifiable();
        }

        [Test]
        public void AddPhoto_ValidFormFile_DoesNotThrows()
        {
            using (var stream = File.OpenRead(@"./Images/valid-image.jpg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"./Images/valid-image.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = GetContentType(Path.GetFileName(@"./Images/valid-image.jpg")),
                };

                Assert.DoesNotThrowAsync(async () => await PhotoService.AddPhoto(file));
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
                    ContentType = GetContentType(fileName),
                };
                Assert.ThrowsAsync<ArgumentException>(async () => await PhotoService.AddPhoto(file));
            }
        }

        [Test]
        public void AddPhotoByURL_ValidResponse()
        {
            SetUpHttpHandlerMock(HttpStatusCode.OK);

            string url = "https://google.com";
            Assert.DoesNotThrowAsync(async () => await PhotoService.AddPhotoByURL(url));
        }

        [Test]
        public void AddPhotoByURL_InvalidResponse()
        {
            SetUpHttpHandlerMock(HttpStatusCode.BadRequest);

            string url = "https://google.com";
            Assert.ThrowsAsync<ArgumentException>(async () => await PhotoService.AddPhotoByURL(url));
        }

        [Test]
        public void Delete_DoesNotThrows()
        {
            Assert.DoesNotThrowAsync(async () => await PhotoService.Delete(photoId));
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
