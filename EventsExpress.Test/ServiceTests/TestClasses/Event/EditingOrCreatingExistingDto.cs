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
            Description = "sjsdnl sdmkskdl dsnlndsl",
            Owners = new List<User>()
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                    },
                },
            PhotoId = Guid.NewGuid(),
            Title = "SLdndsndj",
            IsBlocked = false,
            IsPublic = true,
            Categories = null,
            Point = new Point(10.45, 12.34),
            MaxParticipants = 2147483647,
            Type = LocationType.Map,
        };

        private static EventDto eventDTOOnline = new EventDto
        {
            Id = GetEventExistingId.SecondEventId,
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today,
            Description = "second event",
            Owners = new List<User>()
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                    },
                },
            PhotoId = Guid.NewGuid(),
            Title = "Second",
            IsBlocked = false,
            IsPublic = true,
            Categories = null,
            OnlineMeeting = new Uri("http://basin.example.com/#branch"),
            MaxParticipants = 2147483647,
            Type = LocationType.Online,
        };

        public static EventDto EventDTOMap
        {
            get => eventDTOMap;
        }

        public static EventDto EventDTOOnline
        {
            get => eventDTOOnline;
        }

        public IEnumerator GetEnumerator()
        {
            yield return new object[] { EventDTOMap };
            yield return new object[] { EventDTOOnline };
        }
    }
}
