using System.IO;
using System.Net.Http;
using Azure.Storage.Blobs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Services;
using EventsExpress.Test.ServiceTests.TestClasses.Photo;
using Moq;
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

            mockBlobServiceClient
                .Setup(s => s.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(mockBlobContainer.Object);

            mockBlobContainer
                .Setup(c => c.GetBlobClient(It.IsAny<string>()))
                .Returns(BlobClientMock.Object);
        }

        [Test]
        [TestCase(@"./Images/invalidFile.txt")]
        [TestCase(@"./Images/invalidFile.html")]
        public void IsImage_FalseValidation(string testFilePath)
        {
            using var stream = new MemoryStream();
            var file = PhotoHelpers.GetPhoto(testFilePath, stream);
            Assert.IsFalse(file.IsImage());
        }

        [Test]
        [TestCase(@"./Images/valid-event-image.jpg")]
        public void IsImage_TrueValidation(string testFilePath)
        {
            using var stream = new MemoryStream();
            var file = PhotoHelpers.GetPhoto(testFilePath, stream);
            Assert.IsTrue(file.IsImage());
        }
    }
}
