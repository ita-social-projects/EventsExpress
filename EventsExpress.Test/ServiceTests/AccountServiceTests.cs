using System;
using EventsExpress.Core.Exceptions;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class AccountServiceTests : TestInitializer
    {
        [SetUp]
        protected override void Initialize()
        {
        }

            /* [Test]
            public void Unblock_InvalidUser_ReturnFalse()
            {
                Assert.ThrowsAsync<EventsExpressException>(async () => await service.Unblock(Guid.NewGuid()));
            }

            [Test]
            public void Unblock_AllIsValid_ReturnFalse()
            {
                Assert.DoesNotThrowAsync(async () => await service.Unblock(existingUser.Id));
            }

            [Test]
            public void Block_InvalidUser_ReturnFalse()
            {
                Assert.ThrowsAsync<EventsExpressException>(async () => await service.Block(Guid.NewGuid()));
            }

            [Test]
            public void Block_AllIsValid_ReturnFalse()
            {
                Assert.DoesNotThrowAsync(async () => await service.Block(existingUser.Id));
            }

            [Test]
            public void ChangeRole_InvalidRole_ReturnFalse()
            {
                var roleId = Guid.NewGuid();
                var userId = Guid.NewGuid();

                Assert.ThrowsAsync<EventsExpressException>(async () => await service.ChangeRole(userId, roleId));
            }

            [Test]
            public void ChangeRole_InvalidUser_ReturnFalse()
            {
                var roleId = Guid.NewGuid();
                var userId = Guid.NewGuid();

                Assert.ThrowsAsync<EventsExpressException>(async () => await service.ChangeRole(userId, roleId));
            }

            [Test]
            public void ChangeRole_RoleIdAndUserIdIsValid_ReturnTrue()
            {
                Assert.DoesNotThrowAsync(async () => await service.ChangeRole(userId, roleId));
            } */
        }
}
