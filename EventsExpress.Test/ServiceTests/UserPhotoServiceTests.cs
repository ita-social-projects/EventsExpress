using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventsExpress.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Core.Services
{
    internal class UserPhotoServiceTests : TestInitializer
    {
        public UserPhotoService UserPhotoService { get; set; }

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

            mockBlobServiceClient
                .Setup(s => s.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(mockBlobContainer.Object);

            mockBlobContainer
                .Setup(c => c.GetBlobClient(It.IsAny<string>()))
                .Returns(BlobClientMock.Object);

            UserPhotoService = new UserPhotoService(mockBlobServiceClient.Object);
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

            Assert.DoesNotThrowAsync(async () => await UserPhotoService.AddUserPhoto(file, id));
            BlobClientMock.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()), Times.Once);
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
