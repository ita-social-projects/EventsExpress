using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.Notifications;
using EventsExpress.Hubs;
using EventsExpress.Hubs.Clients;
using EventsExpress.NotificationHandlers;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.HandlerTests
{
    public class CreatedUserHandlerTests
    {
        private Mock<IHubContext<UsersHub, IUsersClient>> hubContextMock;
        private CreatedUserHandler handler;

        [SetUp]
        public void Initialize()
        {
            hubContextMock = new Mock<IHubContext<UsersHub, IUsersClient>>();
            handler = new CreatedUserHandler(hubContextMock.Object);
        }

        [Test]
        public async Task Handle_All_hub_clients_receive_a_number_of_users()
        {
            // Arrange
            hubContextMock.Setup(s => s.Clients.All)
                .Returns(new Mock<IUsersClient>().Object);

            // Act
            await handler.Handle(new CreatedUserMessage(), CancellationToken.None);

            // Assert
            hubContextMock.Verify(s => s.Clients.All.CountUsers(), Times.Once);
        }
    }
}
