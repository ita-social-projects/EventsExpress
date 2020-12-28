using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class InventoryServiceTest : TestInitializer
    {
        private InventoryService service;
        private List<Event> events;
        private List<UnitOfMeasuring> unitOfMeasurings;

        private InventoryDTO inventoryDTO;
        private Inventory inventory;

        private Guid eventId = Guid.NewGuid();
        private Guid inventoryId = Guid.NewGuid();
        private Guid unitOfMeasuringId = Guid.NewGuid();

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();

            service = new InventoryService(
                Context,
                MockMapper.Object);

            unitOfMeasurings = new List<UnitOfMeasuring>
            {
                new UnitOfMeasuring
                {
                    Id = unitOfMeasuringId,
                    ShortName = "Kg",
                    UnitName = "Kilograms",
                },
            };

            inventoryDTO = new InventoryDTO
            {
                Id = inventoryId,
                ItemName = "Happy",
                NeedQuantity = 5,
                UnitOfMeasuring = new UnitOfMeasuringDTO
                {
                    Id = unitOfMeasuringId,
                    ShortName = "Kg",
                    UnitName = "Kilograms",
                },
            };

            inventory = new Inventory
            {
                Id = inventoryId,
                ItemName = "Happy",
                NeedQuantity = 5,
                UnitOfMeasuringId = unitOfMeasuringId,
            };

            events = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    CityId = Guid.NewGuid(),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "...",
                    PhotoId = Guid.NewGuid(),
                    Title = "Title",
                    IsBlocked = false,
                    IsPublic = true,
                    Categories = null,
                    MaxParticipants = 2147483647,
                },
                new Event
                {
                    Id = eventId,
                    CityId = Guid.NewGuid(),
                    DateFrom = DateTime.Today,
                    DateTo = DateTime.Today,
                    Description = "sjsdnl fgr sdmkskdl dsnlndsl",
                    PhotoId = Guid.NewGuid(),
                    Title = "Title",
                    IsBlocked = false,
                    IsPublic = false,
                    Categories = null,
                    MaxParticipants = 2147483647,
                    Visitors = new List<UserEvent>(),
                },
            };

            MockMapper.Setup(u => u.Map<InventoryDTO, Inventory>(It.IsAny<InventoryDTO>()))
               .Returns((InventoryDTO e) => e == null ?
               null :
               new Inventory()
               {
                   Id = e.Id,
                   ItemName = e.ItemName,
                   NeedQuantity = e.NeedQuantity,
                   UnitOfMeasuringId = e.UnitOfMeasuring.Id,
               });

            Context.Inventories.Add(inventory);
            Context.UnitOfMeasurings.AddRange(unitOfMeasurings);
            Context.Events.AddRange(events);
            Context.SaveChanges();
        }

        [Test]
        public void AddInventar_ReturnTrue()
        {
            inventoryDTO.Id = Guid.NewGuid();

            Assert.DoesNotThrowAsync(async () => await service.AddInventar(eventId, inventoryDTO));
        }

        [Test]
        public void DeleteInventar_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.DeleteInventar(inventoryId));
        }

        [Test]
        public void EditInventar_ReturnTrue()
        {
            Assert.DoesNotThrowAsync(async () => await service.EditInventar(inventoryDTO));
        }

        [Test]
        public void GetInventar_InvalidId_ReturnEmptyList()
        {
            var result = service.GetInventar(Guid.NewGuid());

            Assert.IsEmpty(result);
        }

        [Test]
        public void GetInventarById_InvalidId_ReturnEmptyObject()
        {
            var result = service.GetInventarById(Guid.NewGuid());

            InventoryDTO expected = new InventoryDTO();

            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.ItemName, result.ItemName);
            Assert.AreEqual(expected.NeedQuantity, result.NeedQuantity);
            Assert.AreEqual(expected.UnitOfMeasuring, result.UnitOfMeasuring);
        }
    }
}
