using System;
using System.Collections;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;

namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    using NUnit.Framework;

    internal class EditingOrCreatingExistingDto : IEnumerable
    {
        private static Dictionary<string, EventDto> EventDtos => new ()
        {
            ["EventWithMapLocation"] = new EventDto
            {
                Id = EventTestData.FirstEventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "First event",
                Organizers = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                    },
                },
                Title = "First",
                IsPublic = true,
                Categories = new List<CategoryDto>
                {
                    new CategoryDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Category#1",
                    },
                },
                Location = new LocationDto
                {
                    Point = new Point(10.45, 12.34),
                    Type = LocationType.Map,
                },
                MaxParticipants = int.MaxValue,
            },
            ["EventWithOnlineLocation"] = new EventDto
            {
                Id = EventTestData.SecondEventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "Second event",
                Organizers = new List<User>
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
                Categories = new List<CategoryDto>
                {
                    new CategoryDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Category#1",
                    },
                },
                Location = new LocationDto
                {
                    OnlineMeeting = "http://basin.example.com/#branch",
                    Type = LocationType.Online,
                },
                MaxParticipants = int.MaxValue,
            },
            ["DraftOfRecurrentEvent"] = new EventDto
            {
                Id = EventTestData.ThirdEventId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                Description = "Third event",
                Organizers = new List<User>
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
                Categories = new List<CategoryDto>
                {
                    new CategoryDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Category#1",
                    },
                },
                Location = new LocationDto
                {
                    Point = new Point(50.45, 35.34),
                    Type = LocationType.Map,
                },
                MaxParticipants = 14,
            },
        };

        public IEnumerator GetEnumerator()
        {
            foreach (var (key, value) in EventDtos)
            {
                yield return new TestCaseData(value)
                {
                    TestName = $"Case_{key}_ExecutesSuccessfully",
                };
            }
        }
    }
}
