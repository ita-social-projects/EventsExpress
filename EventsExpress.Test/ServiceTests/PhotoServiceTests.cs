using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Services;
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
        public PhotoService PhotoService { get; set; }

        private Mock<BlobClient> BlobClientMock { get; set; }

        private Mock<HttpMessageHandler> HttpMessageHandlerMock { get; set; }

        private Mock<IHttpClientFactory> HttpClientFactoryMock { get; set; }

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            var mockBlobServiceClient = new Mock<BlobServiceClient>();
            var mockBlobContainer = new Mock<BlobContainerClient>();
            BlobClientMock = new Mock<BlobClient>();

            HttpClientFactoryMock = new Mock<IHttpClientFactory>();
            HttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            HttpClientFactoryMock.Setup(f => f.CreateClient(string.Empty))
                .Returns(new HttpClient((HttpMessageHandler)HttpMessageHandlerMock.Object))
                .Verifiable();

            var mockOpt = new Mock<IOptions<ImageOptionsModel>>();
            mockOpt.Setup(opt => opt.Value.Thumbnail).Returns(400);
            mockOpt.Setup(opt => opt.Value.Image).Returns(1200);

            mockBlobServiceClient
                .Setup(s => s.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(mockBlobContainer.Object);

            mockBlobContainer
                .Setup(c => c.GetBlobClient(It.IsAny<string>()))
                .Returns(BlobClientMock.Object);

            PhotoService = new PhotoService(mockOpt.Object, HttpClientFactoryMock.Object, mockBlobServiceClient.Object);
        }

        [Test]
        public void AddEventTempPhoto_DoesntThrowExceptions()
        {
            string testFilePath = @"./Images/valid-image.jpg";
            byte[] bytes = File.ReadAllBytes(testFilePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(testFilePath);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            var file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            Guid id = Guid.NewGuid();

            Assert.DoesNotThrowAsync(async () => await PhotoService.AddEventTempPhoto(file, id));
            BlobClientMock.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Test]
        [TestCase(@"./Images/invalidFile.txt")]
        [TestCase(@"./Images/invalidFile.html")]
        [TestCase(@"./Images/tooSmallImage.jpg")]
        public void AddEventTempPhoto_WithInvalidImages_ShouldThrow(string testFilePath)
        {
            byte[] bytes = File.ReadAllBytes(testFilePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(testFilePath);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            var file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            Guid id = Guid.NewGuid();

            Assert.ThrowsAsync<ArgumentException>(async () => await PhotoService.AddEventTempPhoto(file, id));
            BlobClientMock.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public void AddUserPhoto_ValidFormFile_DoesNotThrows()
        {
            string testFilePath = @"./Images/valid-image.jpg";
            byte[] bytes = File.ReadAllBytes(testFilePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(testFilePath);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            var file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            Guid id = Guid.NewGuid();

            Assert.DoesNotThrowAsync(async () => await PhotoService.AddUserPhoto(file, id));
            BlobClientMock.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        [TestCase(@"./Images/invalidFile.txt")]
        [TestCase(@"./Images/invalidFile.html")]
        [TestCase(@"./Images/tooSmallImage.jpg")]
        public void AddUserPhoto_InValidFormFile_WillThrows(string testFilePath)
        {
            byte[] bytes = File.ReadAllBytes(testFilePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(testFilePath);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(base64));
            var file = new FormFile(stream, 0, stream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            Guid id = Guid.NewGuid();

            Assert.ThrowsAsync<ArgumentException>(async () => await PhotoService.AddUserPhoto(file, id));
            BlobClientMock.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public void GetPhotoFromBlob_DoesNotThrows()
        {
            Assert.DoesNotThrowAsync(async () => await PhotoService.GetPhotoFromAzureBlob($"events/{Guid.NewGuid()}/preview.png"));
            BlobClientMock.Verify(x => x.DownloadToAsync(It.IsAny<MemoryStream>()), Times.Once);
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
