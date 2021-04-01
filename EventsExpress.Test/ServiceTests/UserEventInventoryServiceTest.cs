using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]

    internal class UserEventInventoryServiceTest : TestInitializer
    {
        private UserEventInventoryService service;

        private UserEventInventoryDto userEventInventoryDTO;

        private Event eventEntity;
        private User user;
        private User validUserButNotVisitor;
        private Inventory inventory;

        private Guid eventId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();
        private Guid validUserId = Guid.NewGuid();
        private Guid inventoryId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            service = new UserEventInventoryService(Context, MockMapper.Object);

            userEventInventoryDTO = new UserEventInventoryDto
            {
                EventId = eventId,
                UserId = userId,
                InventoryId = inventoryId,
                Quantity = 3,
            };

            eventEntity = new Event
            {
                Id = eventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "...",
            };

            user = new User
            {
                Id = userId,
                Name = "Name",
                Email = "Email",
                Role = new Db.Entities.Role(),
            };

            validUserButNotVisitor = new User
            {
                Id = validUserId,
                Name = "Name",
                Email = "Email",
                Role = new Db.Entities.Role(),
            };

            inventory = new Inventory
            {
                Id = inventoryId,
                ItemName = "Name",
                NeedQuantity = 3,
                UnitOfMeasuringId = Guid.NewGuid(),
            };

            Context.Users.AddRange(user, validUserButNotVisitor);
            Context.Inventories.Add(inventory);
            Context.UserEvent.Add(new UserEvent
            {
                EventId = eventId,
                UserId = userId,
                Status = Status.WillGo,
                UserStatusEvent = UserStatusEvent.Approved,
            });
            Context.Events.Add(eventEntity);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<UserEventInventoryDto, UserEventInventory>(It.IsAny<UserEventInventoryDto>()))
               .Returns((UserEventInventoryDto e) => e == null ?
               null :
               new UserEventInventory()
               {
                   EventId = e.EventId,
                   UserId = e.UserId,
                   InventoryId = e.InventoryId,
                   Quantity = e.Quantity,
               });
        }

        [Test]
        public void MarkItemAsTakenByUser_DefunctEventId_ThrowExeption()
        {
            userEventInventoryDTO.EventId = Guid.NewGuid();
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.MarkItemAsTakenByUser(userEventInventoryDTO));
        }

        [Test]
        public void MarkItemAsTakenByUser_DefunctUserId_ThrowExeption()
        {
            userEventInventoryDTO.UserId = Guid.NewGuid();
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.MarkItemAsTakenByUser(userEventInventoryDTO));
        }

        [Test]
        public void MarkItemAsTakenByUser_DefunctVisitor_ThrowExeption()
        {
            userEventInventoryDTO.UserId = validUserId;
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.MarkItemAsTakenByUser(userEventInventoryDTO));
        }

        [Test]
        public void MarkItemAsTakenByUser_DefunctInventoryId_ThrowExeption()
        {
            userEventInventoryDTO.InventoryId = Guid.NewGuid();
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.MarkItemAsTakenByUser(userEventInventoryDTO));
        }

        [Test]
        public void MarkItemAsTakenByUser_ExistingDTO_DoesNotThrowExeption()
        {
            Assert.DoesNotThrowAsync(async () => await service.MarkItemAsTakenByUser(userEventInventoryDTO));
        }

        [Test]
        public void Delete_NotExistingObject_ThrowExeption()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Delete(userEventInventoryDTO));
        }

        [Test]
        public void Delete_ExistingObject_DoesNotThrowExeption()
        {
            var entity = Context.UserEventInventories.Add(new UserEventInventory
            {
                EventId = eventId,
                UserId = userId,
                InventoryId = inventoryId,
                Quantity = 3,
            });
            Context.SaveChanges();
            entity.State = EntityState.Detached;

            Assert.DoesNotThrowAsync(async () => await service.Delete(userEventInventoryDTO));
        }

        [Test]
        public void Edit_NotExistingObject_ThrowExeption()
        {
            Assert.ThrowsAsync<EventsExpressException>(async () => await service.Edit(userEventInventoryDTO));
        }

        [Test]
        public void Edit_ExistingObject_DoesNotThrowExeption()
        {
            var entity = Context.UserEventInventories.Add(new UserEventInventory
            {
                EventId = eventId,
                UserId = userId,
                InventoryId = inventoryId,
                Quantity = 2,
            });
            Context.SaveChanges();
            entity.State = EntityState.Detached;

            Assert.DoesNotThrowAsync(async () => await service.Edit(userEventInventoryDTO));
        }
    }
}
