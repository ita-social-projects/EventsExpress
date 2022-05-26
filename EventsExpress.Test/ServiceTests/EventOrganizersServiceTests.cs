using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests;

[TestFixture]
public class EventOrganizersServiceTests : TestInitializer
{
    private EventOrganizersService _service;

    private Guid _organizerId;
    private Guid _visitorId;
    private Guid _eventId;

    [SetUp]
    protected override void Initialize()
    {
        base.Initialize();

        _organizerId = Guid.NewGuid();
        var organizer = new User
        {
            Id = _organizerId,
            FirstName = "Organizer",
            Email = "organizer@gmail.com",
            Birthday = DateTime.Today.AddYears(-20),
        };

        _visitorId = Guid.NewGuid();
        var visitor = new User
        {
            Id = _visitorId,
            FirstName = "Visitor",
            Email = "visitor@gmail.com",
            Birthday = DateTime.Today.AddYears(-20),
        };

        var eventCategoryId = Guid.NewGuid();
        var eventLocationIdMap = Guid.NewGuid();
        var eventLocationMap = new Db.Entities.Location
        {
            Id = eventLocationIdMap, Point = new Point(10.45, 12.34), Type = LocationType.Map,
        };

        _eventId = Guid.NewGuid();
        var @event = new Event
        {
            Id = _eventId,
            Title = "Test Event",
            Description = "None",
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today.AddDays(1),
            IsPublic = true,
            LocationId = eventLocationIdMap,
            MaxParticipants = 2147483647,
            EventSchedule = new EventSchedule
            {
                IsActive = true,
                Frequency = 1,
                Periodicity = Periodicity.Weekly,
                LastRun = DateTime.Today,
                NextRun = DateTime.Today.AddDays(7),
            },
            Categories = new[]
            {
                new EventCategory
                {
                    EventId = _eventId,
                    CategoryId = eventCategoryId,
                    Category = new Category { Id = eventCategoryId, Name = "Meeting" },
                },
            },
            StatusHistory = new[]
            {
                new EventStatusHistory
                {
                    EventStatus = EventStatus.Active, CreatedOn = DateTime.Today,
                },
            },
        };

        Context.Locations.Add(eventLocationMap);
        Context.EventOrganizers.Add(new EventOrganizer { UserId = _organizerId, EventId = _eventId });
        Context.UserEvent.Add(new UserEvent { UserId = _visitorId, EventId = _eventId });
        Context.Users.Add(organizer);
        Context.Events.Add(@event);
        Context.SaveChanges();
        Context.ChangeTracker.Clear();

        _service = new EventOrganizersService(Context);
    }

    [Test]
    public async Task DeleteOrganizerFromEvent_WhenOrganizerExists_ShouldDeleteOrganizer()
    {
        var expectedOrganizersCount = Context.EventOrganizers.Count() - 1;

        await _service.DeleteOrganizerFromEvent(_organizerId, _eventId);
        var actualOrganizersCount = Context.EventOrganizers.Count();

        Assert.AreEqual(expectedOrganizersCount, actualOrganizersCount);
    }

    [Test]
    public async Task PromoteToOrganizer_WhenVisitorBeingPromotedToOrganizer_VisitorShouldBePromoted()
    {
        var expectedOrganizersCount = Context.EventOrganizers.Count() + 1;

        await _service.PromoteToOrganizer(_visitorId, _eventId);
        var actualOrganizersCount = Context.EventOrganizers.Count();

        Assert.AreEqual(expectedOrganizersCount, actualOrganizersCount);
    }
}
