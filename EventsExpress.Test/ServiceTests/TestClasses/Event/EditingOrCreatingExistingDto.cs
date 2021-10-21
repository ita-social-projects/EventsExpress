namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;
    using NetTopologySuite.Geometries;

    public class EditingOrCreatingExistingDto : IEnumerable
    {
        private static EventDto eventDTOMap = new EventDto
        {
            Id = GetEventExistingId.FirstEventId,
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today,
            Description = "First event",
            Owners = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                },
            },
            Title = "First",
            IsPublic = true,
            Categories = new List<CategoryDto>()
            {
                new CategoryDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Category#1",
                },
            },
            Point = new Point(10.45, 12.34),
            MaxParticipants = 2147483647,
            Type = LocationType.Map,
        };

        private static EventDto eventDTOOnline = new EventDto
        {
            Id = GetEventExistingId.SecondEventId,
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today,
            Description = "Second event",
            Owners = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                },
            },
            Title = "Second",
            IsPublic = true,
            IsReccurent = true,
            Frequency = 1,
            Periodicity = Periodicity.Weekly,
            EventStatus = EventStatus.Draft,
            Categories = new List<CategoryDto>()
            {
                new CategoryDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Category#1",
                },
            },
            OnlineMeeting = new Uri("http://basin.example.com/#branch"),
            MaxParticipants = 2147483647,
            Type = LocationType.Online,
        };

        private static EventDto eventDTOReccurentDraft = new EventDto
        {
            Id = GetEventExistingId.ThirdEventId,
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today,
            Description = "Third event",
            Owners = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                },
            },
            Title = "Third",
            IsPublic = true,
            IsReccurent = true,
            Frequency = 1,
            Periodicity = Periodicity.Weekly,
            EventStatus = EventStatus.Draft,
            Categories = new List<CategoryDto>()
            {
                new CategoryDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Category#1",
                },
            },
            Point = new Point(50.45, 35.34),
            MaxParticipants = 14,
            Type = LocationType.Map,
        };

        private static EventDto multiEventDTOReccurentDraft = new EventDto
        {
            Id = GetEventExistingId.ThirdEventId,
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today,
            Description = "Multi event",
            Owners = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                },
            },
            Title = "MultiEvent",
            IsPublic = true,
            IsReccurent = true,
            Frequency = 1,
            Periodicity = Periodicity.Weekly,
            EventStatus = EventStatus.Draft,
            Categories = new List<CategoryDto>()
            {
                new CategoryDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Category#1",
                },
            },
            Point = new Point(50.45, 35.34),
            MaxParticipants = 14,
            Type = LocationType.Map,
            Events = new List<ChildEventDto>()
            {
                new ChildEventDto
        {
            Id = GetEventExistingId.SecondEventId,
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today,
            Description = "Child event",
            Title = "Child",
            Point = new Point(50.45, 35.34),
            Type = LocationType.Map,
        },
            },
        };

        public static EventDto EventDTOMap
        {
            get => eventDTOMap;
        }

        public static EventDto EventDTOOnline
        {
            get => eventDTOOnline;
        }

        public static EventDto EventDTOReccurentDraft
        {
            get => eventDTOReccurentDraft;
        }

        public static EventDto MultiEventDTOReccurentDraft
        {
            get => multiEventDTOReccurentDraft;
        }

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { EventDTOMap };
            yield return new object[] { EventDTOOnline };
            yield return new object[] { EventDTOReccurentDraft };
            yield return new object[] { MultiEventDTOReccurentDraft };
        }
    }
}
