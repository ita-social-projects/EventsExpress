using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using NUnit.Framework;

namespace EventsExpress.Test.GraphQLTests
{
    [TestFixture]
    public class EventTests
    {
        private IRequestExecutor executor;

        private Guid userId = Guid.NewGuid();
        private DateTime eventDateFrom = new DateTime(2022, 12, 1);
        private DateTime eventDateTo = new DateTime(2022, 12, 2);
        private User user = new User();
        private Category firstCategory = new Category { Name = "First category" };
        private Category secondCategory = new Category { Name = "Second category" };
        private Rate rate = new Rate();
        private Inventory inventory = new Inventory();
        private EventStatusHistory statusHistory = new EventStatusHistory();
        private EventSchedule eventSchedule = new EventSchedule();
        private EventAudience eventAudience = new EventAudience();

        private JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter> { new PointJsonConverter() },
        };

        public List<Event> GetEventsFromExecutionResult(IExecutionResult result)
        {
            string resultJson = result.ToJson();
            string node = JsonObject.Parse(resultJson)["data"]["events"]["nodes"].ToString();

            List<Event> eventList = JsonConvert.DeserializeObject<List<Event>>(node, serializerSettings);

            return eventList;
        }

        public void AddTestData(AppDbContext context)
        {
            Event testEvent = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Event",
                Description = "test event",
                DateFrom = eventDateFrom,
                DateTo = eventDateTo,
                IsPublic = true,
                MaxParticipants = 10,
                EventSchedule = eventSchedule,
                EventAudience = eventAudience,
                Rates = new List<Rate> { rate },
                Inventories = new List<Inventory> { inventory },
                StatusHistory = new List<EventStatusHistory> { statusHistory },
                Visitors = new List<UserEvent> { new UserEvent { User = user } },
                Categories = new List<EventCategory>
                {
                    new EventCategory { Category = firstCategory },
                },
            };

            Event testEventFiltering = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Filtered event",
                Description = "Event for filtering test",
                DateFrom = eventDateFrom,
                DateTo = eventDateTo,
                IsPublic = true,
                MaxParticipants = 10,
                EventSchedule = new EventSchedule(),
                EventAudience = new EventAudience(),
                Rates = new List<Rate> { new Rate() },
                Inventories = new List<Inventory> { new Inventory() },
                StatusHistory = new List<EventStatusHistory> { new EventStatusHistory() },
                Visitors = new List<UserEvent> { new UserEvent { User = user } },
                Categories = new List<EventCategory>
                {
                    new EventCategory { Category = firstCategory },
                    new EventCategory { Category = secondCategory },
                },
            };

            Event testEventWithLocation = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Event with location",
                Description = "Event for location filtering test",
                DateFrom = eventDateFrom,
                DateTo = eventDateTo,
                IsPublic = true,
                MaxParticipants = 10,
                EventSchedule = new EventSchedule(),
                EventAudience = new EventAudience(),
                Rates = new List<Rate> { new Rate() },
                Inventories = new List<Inventory> { new Inventory() },
                StatusHistory = new List<EventStatusHistory> { new EventStatusHistory() },
                Visitors = new List<UserEvent> { new UserEvent { User = user } },
                Categories = new List<EventCategory>
                {
                    new EventCategory { Category = firstCategory },
                },
                EventLocation = new EventLocation
                {
                    Point = new Point(48.24354257613047, 26.0101318359375) { SRID = 4326 },
                    Type = LocationType.Map,
                },
            };

            context.Events.Add(testEvent);
            context.Events.Add(testEventFiltering);
            context.Events.Add(testEventWithLocation);
            context.SaveChanges();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            Init().Wait();
        }

        public async Task Init()
        {
            // arrange
            var mockSecurityContext = new Mock<ISecurityContext>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            mockSecurityContext.Setup(x => x.GetCurrentUserId()).Returns(userId);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());

            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "EventExpress").Options;
            AppDbContext context = new AppDbContext(options, mockSecurityContext.Object);
            AddTestData(context);

            executor = await new ServiceCollection()
                .AddDbContextFactory<AppDbContext>(options =>
                    options.UseInMemoryDatabase("EventExpress"))
                .AddSingleton(context)
                .AddSingleton(sp => mockHttpContextAccessor.Object)
                .AddScoped(sp => mockSecurityContext.Object)
                .AddGraphQLService();
        }

        [Test]
        public async Task GetEvents()
        {
            // act
            IExecutionResult result = await executor.ExecuteAsync(TestQueries.GetQuery());

            // assert
            List<Event> eventList = GetEventsFromExecutionResult(result);

            Event ev = eventList.First<Event>();

            Assert.AreEqual("Event", ev.Title);
            Assert.AreEqual("test event", ev.Description);
            Assert.AreEqual(eventDateFrom, ev.DateFrom);
            Assert.AreEqual(eventDateTo, ev.DateTo);
            Assert.IsTrue(ev.IsPublic);
            Assert.AreEqual(10, ev.MaxParticipants);

            Assert.IsTrue(ev.Categories.Any(x => x.Category.Id == firstCategory.Id));
            Assert.IsTrue(ev.Rates.Any(x => x.Id == rate.Id));
            Assert.IsTrue(ev.Inventories.Any(x => x.Id == inventory.Id));
            Assert.IsTrue(ev.StatusHistory.Any(x => x.Id == statusHistory.Id));
            Assert.IsTrue(ev.EventSchedule.Id == eventSchedule.Id);
        }

        [Test]
        public async Task GetEventsByCategoryFilter()
        {
            // act
            IExecutionResult result = await executor.ExecuteAsync(TestQueries.GetQueryWithFilterByCategoryName());

            // assert
            List<Event> eventList = GetEventsFromExecutionResult(result);

            Event ev = eventList.First<Event>();

            Assert.AreEqual("Filtered event", ev.Title);
            Assert.IsTrue(ev.Categories.Any(x => x.Category.Id == secondCategory.Id));
        }

        [Test]
        public async Task GetEventsByLocationFilter()
        {
            // act
            IExecutionResult result = await executor.ExecuteAsync(TestQueries.GetQueryWithFilterByLocationCoordinates());

            // assert
            List<Event> eventList = GetEventsFromExecutionResult(result);

            int filteredEventsCount = eventList.Count;
            Event ev = eventList.First<Event>();

            Assert.AreEqual(1, filteredEventsCount);
            Assert.AreEqual(ev.Title, "Event with location");
        }

        [Test]
        public async Task GetFirstTwoEvents()
        {
            // act
            IExecutionResult result = await executor.ExecuteAsync(TestQueries.GetQueryWithPagingFilter());

            // assert
            List<Event> eventList = GetEventsFromExecutionResult(result);

            int filteredEventsCount = eventList.Count();

            Assert.AreEqual(2, filteredEventsCount);
        }
    }
}
