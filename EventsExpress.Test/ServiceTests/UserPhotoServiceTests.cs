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
using EventsExpress.Test.ServiceTests.TestClasses.Photo;
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
            string testFilePath = @"./Images/valid-user-image.jpg";
            using var stream = new MemoryStream();
            var file = PhotoHelpers.GetPhoto(testFilePath, stream);
            Guid id = Guid.NewGuid();

            Assert.DoesNotThrowAsync(async () => await UserPhotoService.AddUserPhoto(file, id));
            BlobClientMock.Verify(x => x.UploadAsync(It.IsAny<MemoryStream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void GetUserPhoto_GetBytes()
        {
            var result = UserPhotoService.GetUserPhoto(Guid.NewGuid());

            Assert.IsInstanceOf<byte[]>(result.Result);
        }

        [Test]
        public async Task GetUserPhoto_PassEmptyGuid_MustReturnArrayWithZeroSize()
        {
            var res = await UserPhotoService.GetUserPhoto(Guid.Empty);

            Assert.That(res.Length == 0);
        }
    }
}
