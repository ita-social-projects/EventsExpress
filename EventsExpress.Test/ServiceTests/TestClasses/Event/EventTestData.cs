using System;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;

namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    using Event = EventsExpress.Db.Entities.Event;

    internal static class EventTestData
    {
        public static readonly Guid FirstUserId = Guid.NewGuid();
        public static readonly Guid SecondUserId = Guid.NewGuid();

        public static readonly Guid FirstEventId = Guid.NewGuid();
        public static readonly Guid SecondEventId = Guid.NewGuid();
        public static readonly Guid ThirdEventId = Guid.NewGuid();
        public static readonly Guid VisitedEventId = Guid.NewGuid();
        public static readonly Guid PrivateEventId = Guid.NewGuid();
        public static readonly Guid IsPublicNullEventId = Guid.NewGuid();

        public static readonly Guid EventLocationMapId = Guid.NewGuid();
        public static readonly Guid EventLocationMapSecondId = Guid.NewGuid();
        public static readonly Guid EventLocationOnlineId = Guid.NewGuid();

        public static readonly Guid EventOrganizerId = Guid.NewGuid();
        public static readonly Guid EventCategoryId = Guid.NewGuid();

        public static List<User> Users => new ()
        {
            new User
            {
                Id = FirstUserId,
                Name = "NameIsExist",
                Email = "stas@gmail.com",
                Birthday = DateTime.Today.AddYears(-20),
            },
            new User
            {
                Id = SecondUserId,
                Name = "UnderageUser",
                Email = "younguser@gmail.com",
                Birthday = DateTime.Today.AddYears(-16),
            },
        };

        public static List<Rate> Rates => new ()
        {
            new Rate
            {
                EventId = VisitedEventId,
                UserFromId = FirstUserId,
                Score = 6,
            },
            new Rate
            {
                EventId = VisitedEventId,
                UserFromId = Guid.NewGuid(),
                Score = 9,
            },
            new Rate
            {
                EventId = FirstEventId,
                UserFromId = FirstUserId,
                Score = 10,
            },
        };

        public static List<EventLocation> Locations => new ()
        {
            new EventLocation
            {
                Id = EventLocationMapId,
                Point = new Point(10.45, 12.34),
                Type = LocationType.Map,
            },
            new EventLocation
            {
                Id = VisitedEventId,
                Point = new Point(50.45, 30.34),
                Type = LocationType.Map,
            },
            new EventLocation
            {
                Id = EventLocationOnlineId,
                OnlineMeeting = "http://basin.example.com/#branch",
                Type = LocationType.Online,
            },
        };

        public static List<Event> Events => new ()
        {
            new Event
            {
                Id = FirstEventId,
                Title = "First event",
                Description = "Public weekly event with category, map location",
                EventSchedule = new EventSchedule
                {
                    IsActive = true,
                    Frequency = 1,
                    Periodicity = Periodicity.Weekly,
                    LastRun = DateTime.Today,
                    NextRun = DateTime.Today.AddDays(7),
                },
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Organizers = new List<EventOrganizer>
                {
                    new EventOrganizer
                    {
                        UserId = EventOrganizerId,
                    },
                },
                EventLocationId = EventLocationMapId,
                IsPublic = true,
                Categories = new List<EventCategory>
                {
                    new EventCategory
                    {
                        EventId = FirstEventId,
                        CategoryId = EventCategoryId,
                        Category = new Category
                        {
                            Id = EventCategoryId,
                            Name = "Meeting",
                        },
                    },
                },
                MaxParticipants = int.MaxValue,
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory
                    {
                        EventStatus = EventStatus.Active,
                        CreatedOn = DateTime.Today.AddDays(-1),
                    },
                },
            },
            new Event
            {
                Id = SecondEventId,
                Title = "Second event",
                Description = "Public weekly event draft, online location",
                EventSchedule = new EventSchedule
                {
                    IsActive = true,
                    Frequency = 1,
                    Periodicity = Periodicity.Weekly,
                    LastRun = DateTime.Today,
                    NextRun = DateTime.Today.AddDays(7),
                },
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Organizers = new List<EventOrganizer>
                {
                    new EventOrganizer
                    {
                        UserId = FirstUserId,
                    },
                },
                EventLocationId = EventLocationOnlineId,
                IsPublic = true,
                Categories = null,
                MaxParticipants = 25,
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory
                    {
                        EventStatus = EventStatus.Draft,
                        CreatedOn = DateTime.Today,
                    },
                },
            },
            new Event
            {
                Id = ThirdEventId,
                Title = "Third event",
                Description = "Public event for adults, blocked status, map location",
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Organizers = new List<EventOrganizer>
                {
                    new EventOrganizer
                    {
                        UserId = Guid.NewGuid(),
                    },
                },
                EventLocationId = EventLocationMapSecondId,
                IsPublic = true,
                EventAudience = new EventAudience
                {
                    IsOnlyForAdults = true,
                },
                Categories = null,
                MaxParticipants = 8,
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory
                    {
                        EventStatus = EventStatus.Blocked,
                        CreatedOn = DateTime.Today,
                    },
                },
            },
            new Event
            {
                Id = VisitedEventId,
                Title = "Visited event",
                Description = "Future private event with a visitor and rate, online location",
                DateFrom = DateTime.Today.AddDays(1),
                DateTo = DateTime.Today.AddDays(2),
                Organizers = new List<EventOrganizer>
                {
                    new EventOrganizer
                    {
                        UserId = Guid.NewGuid(),
                    },
                },
                EventLocationId = EventLocationOnlineId,
                IsPublic = false,
                Categories = null,
                MaxParticipants = 1,
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory
                    {
                        EventStatus = EventStatus.Active,
                        CreatedOn = DateTime.Today,
                    },
                },
                Visitors = new List<UserEvent>
                {
                    new UserEvent
                    {
                        UserStatusEvent = UserStatusEvent.Pending,
                        Status = Status.WillGo,
                        UserId = FirstUserId,
                        EventId = VisitedEventId,
                    },
                },
                Rates = new List<Rate>
                {
                    new Rate
                    {
                        EventId = VisitedEventId,
                        UserFromId = FirstUserId,
                        Score = 6,
                    },
                },
            },
            new Event
            {
                Id = PrivateEventId,
                Title = "Private event",
                Description = "Private weekly event draft, online location",
                EventSchedule = new EventSchedule
                {
                    IsActive = true,
                    Frequency = 1,
                    Periodicity = Periodicity.Weekly,
                    LastRun = DateTime.Today,
                    NextRun = DateTime.Today.AddDays(7),
                },
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Organizers = new List<EventOrganizer>
                {
                    new EventOrganizer
                    {
                        UserId = FirstUserId,
                    },
                },
                EventLocationId = EventLocationOnlineId,
                IsPublic = false,
                Categories = null,
                MaxParticipants = 25,
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory
                    {
                        EventStatus = EventStatus.Draft,
                        CreatedOn = DateTime.Today,
                    },
                },
            },
            new Event
            {
                Id = IsPublicNullEventId,
                Title = "Public null event",
                Description = "Weekly event draft, Is Public null, online location",
                EventSchedule = new EventSchedule
                {
                    IsActive = true,
                    Frequency = 1,
                    Periodicity = Periodicity.Weekly,
                    LastRun = DateTime.Today,
                    NextRun = DateTime.Today.AddDays(7),
                },
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Organizers = new List<EventOrganizer>
                {
                    new EventOrganizer
                    {
                        UserId = FirstUserId,
                    },
                },
                EventLocationId = EventLocationOnlineId,
                IsPublic = null,
                Categories = null,
                MaxParticipants = 25,
                StatusHistory = new List<EventStatusHistory>
                {
                    new EventStatusHistory
                    {
                        EventStatus = EventStatus.Draft,
                        CreatedOn = DateTime.Today,
                    },
                },
            },
        };

        public static PaginationViewModel PageModel => new () { PageSize = 6, Page = 1 };
    }
}
