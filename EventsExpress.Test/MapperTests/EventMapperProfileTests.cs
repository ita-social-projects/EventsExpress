using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Mapping;
using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
using EventsExpress.ViewModels;
using EventsExpress.ViewModels.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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

        private Guid idEventShedule = Guid.NewGuid();
        private Guid idEvent = Guid.NewGuid();
        private Guid idEventLocation = Guid.NewGuid();
        private Guid idEventAudience = Guid.NewGuid();
        private Guid idUser = Guid.NewGuid();
        private Guid idCategory = Guid.NewGuid();
        private Guid idInventory = Guid.NewGuid();
        private Guid idUnitOfMeasuring = Guid.NewGuid();
        private Guid idEventEditViewModel = Guid.NewGuid();
        private string categoryName = "category name";

        private List<User> GetListUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Id = idUser,
                    FirstName = "User",
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
                Title = "First event",
                Description = "It is the first event for testing",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now,
                IsPublic = true,
                MaxParticipants = 8,
                LocationId = idEventLocation,
                EventAudienceId = idEventAudience,
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
                Location = new Db.Entities.Location
                {
                    Id = idEventLocation,
                    Type = LocationType.Map,
                    Point = new Point(8, 3),
                },
                EventAudience = new EventAudience
                {
                    Id = idEventAudience,
                    IsOnlyForAdults = true,
                },
                Organizers = new List<EventOrganizer>()
                {
                    new EventOrganizer
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
                Organizers = users,
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

                Location = new LocationDto
                {
                    Type = LocationType.Map,
                    Point = new Point(8, 3),
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
                MaxParticipants = 8,
                IsOnlyForAdults = true,
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
                Organizers = new List<UserPreviewViewModel>()
                {
                    new UserPreviewViewModel
                    {
                        Id = idUser,
                        FirstName = "name of user",
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
                Organizers = new List<UserPreviewViewModel>()
                {
                    new UserPreviewViewModel
                    {
                        Id = idUser,
                        FirstName = "name of user",
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

            IServiceCollection services = new ServiceCollection();
            var mockAuth = new Mock<IAuthService>();
            var mockUser = new Mock<IUserService>();
            var mockHttpAccessor = new Mock<IHttpContextAccessor>();
            var mockSecurityContextService = new Mock<ISecurityContext>();

            mockHttpAccessor.Setup(o => o.HttpContext.User);

            services.AddTransient(sp => mockAuth.Object);
            services.AddTransient(sp => mockUser.Object);
            services.AddTransient(sp => mockHttpAccessor.Object);
            services.AddTransient(sp => mockSecurityContextService.Object);

            services.AddAutoMapper(typeof(EventMapperProfile));
            services.AddAutoMapper(typeof(UserMapperProfile));

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Mapper = serviceProvider.GetService<IMapper>();
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
            Assert.That(e.Location.Point, Is.EqualTo(firstEvent.Location.Point));
            Assert.That(e.Location.Type, Is.EqualTo(firstEvent.Location.Type));
            Assert.That(e.Location.OnlineMeeting, Is.EqualTo(firstEvent.Location.OnlineMeeting));
            Assert.That(e.Organizers, Has.All.Matches<User>(ex =>
                                                        firstEvent.Organizers
                                                        .All(f =>
                                                            ex.Id == f.User.Id)));
            Assert.That(e.Categories, Has.All.Matches<CategoryDto>(ex =>
                                                       firstEvent.Categories
                                                       .All(f =>
                                                           ex.Id == f.Category.Id &&
                                                           ex.Name == f.Category.Name)));
            Assert.That(e.Frequency, Is.EqualTo(firstEvent.EventSchedule.Frequency));
            Assert.That(e.Periodicity, Is.EqualTo(firstEvent.EventSchedule.Periodicity));
            Assert.That(e.IsReccurent, Is.EqualTo(firstEvent.EventSchedule != null));
            Assert.That(e.Inventories, Has.All.Matches<InventoryDto>(ex =>
                                                       firstEvent.Inventories
                                                       .All(f =>
                                                           ex.Id == f.Id &&
                                                           ex.ItemName == f.ItemName &&
                                                           ex.NeedQuantity == f.NeedQuantity &&
                                                           ex.UnitOfMeasuring.Id == f.UnitOfMeasuring.Id)));
            Assert.That(e.OrganizerIds, Is.Null);
            Assert.That(e.IsOnlyForAdults, Is.EqualTo(firstEvent.EventAudience.IsOnlyForAdults));
        }

        [Test]
        public void EventMapperProfile_EventDtoToEvent()
        {
            firstEventDto = GetEventDto();
            var resEven = Mapper.Map<EventDto, Event>(firstEventDto);
            Assert.That(resEven.Organizers, Has.All.Matches<EventOrganizer>(ex =>
                                                        firstEventDto.Organizers
                                                        .All(f =>
                                                            ex.UserId == f.Id))
                                        .And
                                        .All.Matches<EventOrganizer>(e => firstEventDto.Id == e.EventId));
            Assert.That(resEven.Inventories, Has.All.Matches<Inventory>(ex =>
                                                      firstEventDto.Inventories
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.ItemName == f.ItemName &&
                                                          ex.NeedQuantity == f.NeedQuantity &&
                                                          ex.UnitOfMeasuringId == f.UnitOfMeasuring.Id)));
            Assert.That(resEven.Visitors, Is.Null);
            Assert.That(resEven.Categories, Is.Null);
            Assert.That(resEven.LocationId, Is.Null);
            Assert.That(resEven.Location, Is.Null);
            Assert.That(resEven.EventAudienceId, Is.Null);
            Assert.That(resEven.EventAudience, Is.Null);
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

            Assert.That(resEven.Categories, Has.All.Matches<CategoryViewModel>(ex =>
                                                      firstEventDto.Categories
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.Name == f.Name)));
            Assert.That(resEven.Location.Type, Is.EqualTo(firstEventDto.Location.Type));
            Assert.That(resEven.Location.OnlineMeeting, Is.EqualTo(firstEventDto.Location.OnlineMeeting));
            Assert.That(resEven.Location.Latitude, Is.EqualTo(firstEventDto.Location.Point.X));
            Assert.That(resEven.Location.Longitude, Is.EqualTo(firstEventDto.Location.Point.Y));
            Assert.That(resEven.CountVisitor, Is.EqualTo(visitorCount));
            Assert.That(resEven.MaxParticipants, Is.EqualTo(firstEventDto.MaxParticipants));
            Assert.That(resEven.Members, Has.All.Matches<UserPreviewViewModel>(ex =>
                                                      firstEventDto.Visitors
                                                      .All(f =>
                                                          ex.Id == f.User.Id &&
                                                          ex.FirstName == f.User.FirstName &&
                                                          ex.Birthday == f.User.Birthday &&
                                                          ex.UserStatusEvent == f.UserStatusEvent)));
            Assert.That(resEven.Organizers, Has.All.Matches<UserPreviewViewModel>(ex =>
                                                       firstEventDto.Organizers
                                                       .All(f =>
                                                           ex.Id == f.Id &&
                                                           ex.Birthday == f.Birthday &&
                                                           ex.FirstName == f.FirstName)));
        }

        [Test]
        public void EventMapperProfile_EventDtoToEventViewModel()
        {
            firstEventDto = GetEventDto();
            var resView = Mapper.Map<EventDto, EventViewModel>(firstEventDto);

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
            Assert.That(resView.Location.Type, Is.EqualTo(firstEventDto.Location.Type));
            Assert.That(resView.Location.OnlineMeeting, Is.EqualTo(firstEventDto.Location.OnlineMeeting));
            Assert.That(resView.Location.Latitude, Is.EqualTo(firstEventDto.Location.Point.X));
            Assert.That(resView.Location.Longitude, Is.EqualTo(firstEventDto.Location.Point.Y));
            Assert.That(resView.Visitors, Has.All.Matches<UserPreviewViewModel>(ex =>
                                                      firstEventDto.Visitors
                                                      .All(f =>
                                                          ex.Id == f.User.Id &&
                                                          ex.FirstName == f.User.FirstName &&
                                                          ex.Birthday == f.User.Birthday &&
                                                          ex.UserStatusEvent == f.UserStatusEvent)));
            Assert.That(resView.Organizers, Has.All.Matches<UserPreviewViewModel>(ex =>
                                                      firstEventDto.Organizers
                                                      .All(f =>
                                                          ex.Id == f.Id &&
                                                          ex.Birthday == f.Birthday &&
                                                          ex.FirstName == f.FirstName)));
            Assert.That(resView.Frequency, Is.EqualTo(firstEventDto.Frequency));
            Assert.That(resView.Periodicity, Is.EqualTo(firstEventDto.Periodicity));
            Assert.That(resView.IsReccurent, Is.EqualTo(firstEventDto.IsReccurent));
            Assert.That(resView.MaxParticipants, Is.EqualTo(firstEventDto.MaxParticipants));
            Assert.That(resView.IsOnlyForAdults, Is.EqualTo(firstEventDto.IsOnlyForAdults));
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
            Assert.That(resDto.Organizers, Is.Null);
            Assert.That(resDto.OrganizerIds, Has.All.Matches<Guid>(ex =>
                                                      firstEventEditViewModel.Organizers
                                                      .All(f =>
                                                          ex == f.Id)));
            Assert.That(resDto.Location.Point, Is.EqualTo(firstEventEditViewModel.Location.Type == LocationType.Map ?
                 new Point(firstEventEditViewModel.Location.Latitude.Value, firstEventEditViewModel.Location.Longitude.Value) { SRID = 4326 } : null));
            Assert.That(resDto.Location.OnlineMeeting, Is.EqualTo(firstEventEditViewModel.Location.Type == LocationType.Online ?
                 firstEventEditViewModel.Location.OnlineMeeting : null));
            Assert.That(resDto.Location.Type, Is.EqualTo(firstEventEditViewModel.Location.Type));
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
            Assert.That(resDto.Organizers, Is.Null);
            Assert.That(resDto.OrganizerIds, Has.All.Matches<Guid>(ex =>
                                                      firstEventCreateViewModel.Organizers
                                                      .All(f =>
                                                          ex == f.Id)));
            Assert.That(resDto.Location.Point, Is.EqualTo(firstEventCreateViewModel.Location.Type == LocationType.Map ?
                 new Point(firstEventCreateViewModel.Location.Latitude.Value, firstEventCreateViewModel.Location.Longitude.Value) { SRID = 4326 } : null));
            Assert.That(resDto.Location.OnlineMeeting, Is.EqualTo(firstEventCreateViewModel.Location.Type == LocationType.Online ?
                 firstEventCreateViewModel.Location.OnlineMeeting : null));
            Assert.That(resDto.Location.Type, Is.EqualTo(firstEventCreateViewModel.Location.Type));
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
            Assert.That(resDto.Visitors, Is.Null);
        }
    }
}
