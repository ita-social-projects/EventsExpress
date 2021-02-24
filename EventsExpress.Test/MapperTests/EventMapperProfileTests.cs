using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using EventsExpress.ViewModels.Base;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace EventsExpress.Test.MapperTests
{
    [TestFixture]
    internal class EventMapperProfileTests : MapperTestInitializer<EventMapperProfile>
    {
        private Event firstEvent;
        private EventDto firstEventDto;
        private EventEditViewModel firstEventEditViewModel;
        private EventCreateViewModel firstEventCreateViewModel;
        private Guid idPhoto = Guid.NewGuid();
        private Guid idEventShedule = Guid.NewGuid();
        private Guid idEvent = Guid.NewGuid();
        private Guid idEventLocation = Guid.NewGuid();
        private Guid idUser = Guid.NewGuid();
        private Guid idCategory = Guid.NewGuid();
        private Guid idInventory = Guid.NewGuid();
        private Guid idUnitOfMeasuring = Guid.NewGuid();
        private Guid idEventEditViewModel = Guid.NewGuid();
        private string categoryName = "category name";
        private string photoUrl = "http://basin.example.com/#branch";

        private List<User> GetListUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Id = idUser,
                    Name = "User",
                    Email = "user@gmail.com",
                    Birthday = DateTime.Now,
                },
            };
        }

        private Event GetEvent()
        {
            List<User> users = GetListUsers();
            return new Event
            {
                Id = idEvent,
                IsBlocked = false,
                Title = "First event",
                Description = "It is the first event for testing",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                IsPublic = true,
                MaxParticipants = 8,
                PhotoId = idPhoto,
                EventLocationId = idEventLocation,
                EventSchedule = new EventSchedule
                {
                    Id = idEventShedule,
                    Frequency = 5,
                    LastRun = DateTime.Now,
                    NextRun = DateTime.Now,
                    Periodicity = Periodicity.Daily,
                    IsActive = true,
                    EventId = idEvent,
                    Event = new Event(),
                },
                Photo = new Photo
                {
                    Id = idPhoto,
                    Thumb = new byte[8],
                    Img = new byte[8],
                },
                EventLocation = new EventLocation
                {
                    Id = idEventLocation,
                    Type = LocationType.Map,
                    Point = new Point(8, 3),
                },
                Owners = new List<EventOwner>()
                {
                    new EventOwner
                    {
                        UserId = Guid.NewGuid(),
                        User = users[0],
                        Event = new Event { Id = idEvent },
                        EventId = idEvent,
                    },
                },
                Visitors = new List<UserEvent>()
                {
                    new UserEvent
                    {
                        UserStatusEvent = UserStatusEvent.Pending,
                        Status = Status.WillGo,
                        UserId = idUser,
                        User = users[0],
                        EventId = idEvent,
                    },
                },
                Categories = new List<EventCategory>()
                {
                    new EventCategory
                    {
                        EventId = idEvent,
                        Event = new Event
                        {
                            Id = idEvent,
                        },
                        CategoryId = idCategory,
                        Category = new Category
                        {
                            Id = idCategory,
                            Name = categoryName,
                        },
                    },
                },
                Inventories = new List<Inventory>()
                {
                    new Inventory
                    {
                        Id = idInventory,
                        ItemName = "Inventory name",
                        NeedQuantity = 8.8,
                        UnitOfMeasuring = new UnitOfMeasuring
                        {
                            Id = idUnitOfMeasuring,
                            UnitName = "Unit name",
                            ShortName = "S/n",
                        },
                    },
                },
            };
        }

        private EventDto GetEventDto()
        {
            List<User> users = GetListUsers();
            return new EventDto
            {
                Id = idEvent,
                Owners = users,
                Inventories = new List<InventoryDto>()
                {
                    new InventoryDto
                    {
                        Id = idInventory,
                        ItemName = "Inventory name",
                        NeedQuantity = 8.8,
                        UnitOfMeasuring = new UnitOfMeasuringDto
                        {
                            Id = idUnitOfMeasuring,
                            UnitName = "Unit name",
                            ShortName = "S/n",
                        },
                    },
                },
                PhotoUrl = "http://basin.example.com/#branch",
                Categories = new List<CategoryDto>
                {
                    new CategoryDto
                    {
                       Id = idCategory,
                       Name = categoryName,
                       CountOfEvents = 8,
                       CountOfUser = 8,
                    },
                },

                Type = LocationType.Map,
                Point = new Point(8, 3),
                Visitors = new List<UserEvent>()
                {
                    new UserEvent
                    {
                        UserStatusEvent = UserStatusEvent.Pending,
                        Status = Status.WillGo,
                        UserId = idUser,
                        User = users[0],
                        EventId = idEvent,
                    },
                },
                MaxParticipants = 8,
                PhotoBytes = new Photo { Id = idPhoto, Img = new byte[8], Thumb = new byte[8] },
            };
        }

        private EventEditViewModel GetEventEditViewModel()
        {
            return new EventEditViewModel
            {
                Id = idEventEditViewModel,
                Categories = new List<CategoryViewModel>
                {
                    new CategoryViewModel
                    {
                        Id = idCategory,
                        Name = categoryName,
                    },
                },
                Inventories = new List<InventoryViewModel>()
                {
                    new InventoryViewModel
                    {
                        Id = idInventory,
                        ItemName = "Inventory name",
                        NeedQuantity = 8.8,
                        UnitOfMeasuring = new UnitOfMeasuringViewModel
                        {
                            Id = idUnitOfMeasuring,
                            UnitName = "Unit name",
                            ShortName = "S/n",
                        },
                    },
                },
                Owners = new List<UserPreviewViewModel>()
                {
                    new UserPreviewViewModel
                    {
                        Id = idUser,
                        PhotoUrl = photoUrl,
                        Username = "name of user",
                        Email = "user@gmail.com",
                        Birthday = DateTime.Now,
                        Rating = 8,
                    },
                },
                Location = new LocationViewModel
                {
                    Type = LocationType.Online,
                    OnlineMeeting = "http://basin.example.com/#branch",
                },
                Title = "Some title",
                Description = "Some desc",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                IsReccurent = true,
                Frequency = 1,
                Periodicity = Periodicity.Daily,
                MaxParticipants = 20,
            };
        }

        private EventCreateViewModel GetEventCreateViewModel()
        {
            return new EventCreateViewModel
            {
                Categories = new List<CategoryViewModel>
                {
                    new CategoryViewModel
                    {
                        Id = idCategory,
                        Name = categoryName,
                    },
                },
                Owners = new List<UserPreviewViewModel>()
                {
                    new UserPreviewViewModel
                    {
                        Id = idUser,
                        PhotoUrl = photoUrl,
                        Username = "name of user",
                        Email = "user@gmail.com",
                        Birthday = DateTime.Now,
                        Rating = 8,
                    },
                },
                Location = new LocationViewModel
                {
                    Type = LocationType.Map,
                    Latitude = 0.8,
                    Longitude = 99.87,
                },
                Inventories = new List<InventoryViewModel>()
                {
                    new InventoryViewModel
                    {
                        Id = idInventory,
                        ItemName = "Inventory name",
                        NeedQuantity = 8.8,
                        UnitOfMeasuring = new UnitOfMeasuringViewModel
                        {
                            Id = idUnitOfMeasuring,
                            UnitName = "Unit name",
                            ShortName = "S/n",
                        },
                    },
                },
                Title = "Some title",
                Description = "Some desc",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                IsReccurent = true,
                Frequency = 1,
                Periodicity = Periodicity.Daily,
                MaxParticipants = 20,
            };
        }

        [OneTimeSetUp]

        protected virtual void Init()
        {
            Initialize();
        }

        [Test]
        public void EventMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void EventMapperProfile_EventToEventDto()
        {
            firstEvent = GetEvent();
            var e = Mapper.Map<Event, EventDto>(firstEvent);
            Assert.That(e.Photo, Is.Null);
            Assert.That(e.Point, Is.EqualTo(firstEvent.EventLocation.Point));
            Assert.That(e.Type, Is.EqualTo(firstEvent.EventLocation.Type));
            Assert.That(e.OnlineMeeting, Is.EqualTo(firstEvent.EventLocation.OnlineMeeting));
            Assert.That(e.Owners, Has.All.Matches<User>(ex =>
                                                        firstEvent.Owners
                                                        .All(f =>
                                                            ex.Id == f.User.Id)));
            Assert.That(e.Categories, Has.All.Matches<CategoryDto>(ex =>
                                                       firstEvent.Categories
                                                       .All(f =>
                                                           ex.Id == f.Category.Id &&
                                                           ex.Name == f.Category.Name)));
            Assert.That(e.PhotoBytes, Is.EqualTo(firstEvent.Photo));
            Assert.That(e.Frequency, Is.EqualTo(firstEvent.EventSchedule.Frequency));
            Assert.That(e.Periodicity, Is.EqualTo(firstEvent.EventSchedule.Periodicity));
            Assert.That(e.IsReccurent, Is.EqualTo(firstEvent.EventSchedule != null));
            Assert.That(e.PhotoId, Is.EqualTo(firstEvent.PhotoId));
            Assert.That(e.Inventories, Has.All.Matches<InventoryDto>(ex =>
                                                       firstEvent.Inventories
                                                       .All(f =>
                                                           ex.Id == f.Id &&
                                                           ex.ItemName == f.ItemName &&
                                                           ex.NeedQuantity == f.NeedQuantity &&
                                                           ex.UnitOfMeasuring.Id == f.UnitOfMeasuring.Id)));
            Assert.That(e.PhotoUrl, Is.Null);
            Assert.That(e.OwnerIds, Is.Null);
        }

        [Test]
        public void EventMapperProfile_EventDtoToEvent()
        {
            firstEventDto = GetEventDto();
            var resEven = Mapper.Map<EventDto, Event>(firstEventDto);
            Assert.That(resEven.Owners, Has.All.Matches<EventOwner>(ex =>
                                                        firstEventDto.Owners
                                                        .All(f =>
                                                            ex.UserId == f.Id))
                                        .And
                                        .All.Matches<EventOwner>(e => firstEventDto.Id == e.EventId));
            Assert.That(resEven.Inventories, Has.All.Matches<Inventory>(ex =>
                                                      firstEventDto.Inventories
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.ItemName == f.ItemName &&
                                                          ex.NeedQuantity == f.NeedQuantity &&
                                                          ex.UnitOfMeasuringId == f.UnitOfMeasuring.Id)));
            Assert.That(resEven.Photo, Is.Null);
            Assert.That(resEven.Visitors, Is.Null);
            Assert.That(resEven.Categories, Is.Null);
            Assert.That(resEven.EventLocationId, Is.EqualTo(default(Guid)));
            Assert.That(resEven.EventLocation, Is.Null);
            Assert.That(resEven.EventSchedule, Is.Null);
            Assert.That(resEven.Rates, Is.Null);
            Assert.That(resEven.StatusHistory, Is.Null);
        }

        [Test]
        public void EventMapperProfile_EventDtoToEventPreviewViewModel()
        {
            firstEventDto = GetEventDto();
            var resEven = Mapper.Map<EventDto, EventPreviewViewModel>(firstEventDto);
            var visitorCount = firstEventDto.Visitors.Count(x => x.UserStatusEvent == 0);
            Assert.That(resEven.PhotoUrl, Is.EqualTo(firstEventDto.PhotoBytes.Thumb.ToRenderablePictureString()));
            Assert.That(resEven.Categories, Has.All.Matches<CategoryViewModel>(ex =>
                                                      firstEventDto.Categories
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.Name == f.Name)));
            Assert.That(resEven.Location.Type, Is.EqualTo(firstEventDto.Type));
            Assert.That(resEven.Location.OnlineMeeting, Is.EqualTo(firstEventDto.OnlineMeeting));
            Assert.That(resEven.Location.Latitude, Is.EqualTo(firstEventDto.Point.X));
            Assert.That(resEven.Location.Longitude, Is.EqualTo(firstEventDto.Point.Y));
            Assert.That(resEven.CountVisitor, Is.EqualTo(visitorCount));
            Assert.That(resEven.MaxParticipants, Is.EqualTo(firstEventDto.MaxParticipants));
            Assert.That(resEven.Owners, Has.All.Matches<UserPreviewViewModel>(ex =>
                                                       firstEventDto.Owners
                                                       .All(f =>
                                                           ex.Id == f.Id &&
                                                           ex.PhotoUrl == (f.Photo != null ? f.Photo.Thumb.ToRenderablePictureString() : null) &&
                                                           ex.Birthday == f.Birthday &&
                                                           ex.Username == f.Name)));
        }

        [Test]
        public void EventMapperProfile_EventDtoToEventViewModel()
        {
            firstEventDto = GetEventDto();
            var resView = Mapper.Map<EventDto, EventViewModel>(firstEventDto);
            Assert.That(resView.PhotoUrl, Is.EqualTo(firstEventDto.PhotoBytes.Img.ToRenderablePictureString()));
            Assert.That(resView.Categories, Has.All.Matches<CategoryViewModel>(ex =>
                                                      firstEventDto.Categories
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.Name == f.Name)));
            Assert.That(resView.Inventories, Has.All.Matches<InventoryViewModel>(ex =>
                                                      firstEventDto.Inventories
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.ItemName == f.ItemName &&
                                                          ex.NeedQuantity == f.NeedQuantity &&
                                                          ex.UnitOfMeasuring.Id == f.UnitOfMeasuring.Id &&
                                                          ex.UnitOfMeasuring.ShortName == f.UnitOfMeasuring.ShortName &&
                                                          ex.UnitOfMeasuring.UnitName == f.UnitOfMeasuring.UnitName)));
            Assert.That(resView.Location.Type, Is.EqualTo(firstEventDto.Type));
            Assert.That(resView.Location.OnlineMeeting, Is.EqualTo(firstEventDto.OnlineMeeting));
            Assert.That(resView.Location.Latitude, Is.EqualTo(firstEventDto.Point.X));
            Assert.That(resView.Location.Longitude, Is.EqualTo(firstEventDto.Point.Y));
            Assert.That(resView.Visitors, Has.All.Matches<UserPreviewViewModel>(ex =>
                                                      firstEventDto.Visitors
                                                      .All(f =>
                                                          ex.Id == f.User.Id &&
                                                          ex.Username == f.User.Name &&
                                                          ex.Birthday == f.User.Birthday &&
                                                          ex.PhotoUrl == (f.User.Photo != null ? f.User.Photo.Thumb.ToRenderablePictureString() : null) &&
                                                          ex.UserStatusEvent == f.UserStatusEvent)));
            Assert.That(resView.Owners, Has.All.Matches<UserPreviewViewModel>(ex =>
                                                      firstEventDto.Owners
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.Birthday == f.Birthday &&
                                                          ex.PhotoUrl == (f.Photo != null ? f.Photo.Thumb.ToRenderablePictureString() : null) &&
                                                          ex.Username == f.Name)));
            Assert.That(resView.Frequency, Is.EqualTo(firstEventDto.Frequency));
            Assert.That(resView.Periodicity, Is.EqualTo(firstEventDto.Periodicity));
            Assert.That(resView.IsReccurent, Is.EqualTo(firstEventDto.IsReccurent));
            Assert.That(resView.MaxParticipants, Is.EqualTo(firstEventDto.MaxParticipants));
        }

        [Test]
        public void EventMapperProfile_EventEditViewModelToEventDto()
        {
            firstEventEditViewModel = GetEventEditViewModel();
            var resDto = Mapper.Map<EventEditViewModel, EventDto>(firstEventEditViewModel);
            Assert.That(resDto.Categories, Has.All.Matches<CategoryDto>(ex =>
                                                      firstEventEditViewModel.Categories
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.Name == f.Name)));
            Assert.That(resDto.Inventories, Has.All.Matches<InventoryDto>(ex =>
                                                      firstEventEditViewModel.Inventories
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.ItemName == f.ItemName &&
                                                          ex.NeedQuantity == f.NeedQuantity &&
                                                          ex.UnitOfMeasuring.Id == f.UnitOfMeasuring.Id &&
                                                          ex.UnitOfMeasuring.ShortName == f.UnitOfMeasuring.ShortName &&
                                                          ex.UnitOfMeasuring.UnitName == f.UnitOfMeasuring.UnitName)));
            Assert.That(resDto.Owners, Is.Null);
            Assert.That(resDto.OwnerIds, Has.All.Matches<Guid>(ex =>
                                                      firstEventEditViewModel.Owners
                                                      .All(f =>
                                                          ex == f.Id)));
            Assert.That(resDto.Point, Is.EqualTo(firstEventEditViewModel.Location.Type == LocationType.Map ?
                 new Point(firstEventEditViewModel.Location.Latitude.Value, firstEventEditViewModel.Location.Longitude.Value) { SRID = 4326 } : null));
            Assert.That(resDto.OnlineMeeting, Is.EqualTo(firstEventEditViewModel.Location.Type == LocationType.Online ?
                 new Uri(firstEventEditViewModel.Location.OnlineMeeting) : null));
            Assert.That(resDto.Type, Is.EqualTo(firstEventEditViewModel.Location.Type));
            Assert.That(resDto.IsBlocked, Is.EqualTo(default(bool)));
            Assert.That(resDto.PhotoBytes, Is.EqualTo(default(string)));
            Assert.That(resDto.Visitors, Is.EqualTo(default(string)));
        }

        [Test]
        public void EventMapperProfile_EventCreateViewModelToEventDto()
        {
            firstEventCreateViewModel = GetEventCreateViewModel();
            var resDto = Mapper.Map<EventCreateViewModel, EventDto>(firstEventCreateViewModel);
            Assert.That(resDto.Categories, Has.All.Matches<CategoryDto>(ex =>
                                                     firstEventCreateViewModel.Categories
                                                     .All(f =>
                                                         ex.Id == f.Id &&
                                                         ex.Name == f.Name)));
            Assert.That(resDto.Owners, Is.Null);
            Assert.That(resDto.OwnerIds, Has.All.Matches<Guid>(ex =>
                                                      firstEventCreateViewModel.Owners
                                                      .All(f =>
                                                          ex == f.Id)));
            Assert.That(resDto.Point, Is.EqualTo(firstEventCreateViewModel.Location.Type == LocationType.Map ?
                 new Point(firstEventCreateViewModel.Location.Latitude.Value, firstEventCreateViewModel.Location.Longitude.Value) { SRID = 4326 } : null));
            Assert.That(resDto.OnlineMeeting, Is.EqualTo(firstEventCreateViewModel.Location.Type == LocationType.Online ?
                 new Uri(firstEventCreateViewModel.Location.OnlineMeeting) : null));
            Assert.That(resDto.Type, Is.EqualTo(firstEventCreateViewModel.Location.Type));
            Assert.That(resDto.Periodicity, Is.EqualTo(firstEventCreateViewModel.Periodicity));
            Assert.That(resDto.IsReccurent, Is.EqualTo(firstEventCreateViewModel.IsReccurent));
            Assert.That(resDto.Inventories, Has.All.Matches<InventoryDto>(ex =>
                                                     firstEventCreateViewModel.Inventories
                                                     .All(f =>
                                                         ex.Id == f.Id &&
                                                         ex.ItemName == f.ItemName &&
                                                         ex.NeedQuantity == f.NeedQuantity &&
                                                         ex.UnitOfMeasuring.Id == f.UnitOfMeasuring.Id &&
                                                         ex.UnitOfMeasuring.ShortName == f.UnitOfMeasuring.ShortName &&
                                                         ex.UnitOfMeasuring.UnitName == f.UnitOfMeasuring.UnitName)));
            Assert.That(resDto.Id, Is.EqualTo(default(Guid)));
            Assert.That(resDto.IsBlocked, Is.EqualTo(default(bool)));
            Assert.That(resDto.PhotoUrl, Is.Null);
            Assert.That(resDto.PhotoBytes, Is.Null);
            Assert.That(resDto.Visitors, Is.Null);
        }
    }
}
