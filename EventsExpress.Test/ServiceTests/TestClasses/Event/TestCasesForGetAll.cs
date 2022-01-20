using System;
using System.Collections;
using System.Collections.Generic;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests.TestClasses.Event
{
    internal class TestCasesForGetAll : IEnumerable
    {
        private static Dictionary<string, EventFilterViewModel> Models => new ()
        {
            ["KeyWord"] = new EventFilterViewModel
            {
                KeyWord = "Third",
            },
            ["Date"] = new EventFilterViewModel
            {
                DateFrom = DateTime.Today.AddDays(1),
                DateTo = DateTime.Today.AddDays(5),
            },
            ["OrganizerId"] = new EventFilterViewModel
            {
                OrganizerId = EventTestData.EventOrganizerId,
            },
            ["VisitorId"] = new EventFilterViewModel
            {
                VisitorId = EventTestData.FirstUserId,
            },
            ["Location"] = new EventFilterViewModel
            {
                X = 10.45,
                Y = 12.34,
                Radius = 8,
            },
            ["LocationType"] = new EventFilterViewModel
            {
                LocationType = LocationType.Online,
            },
            ["Adults"] = new EventFilterViewModel
            {
                IsOnlyForAdults = true,
            },
            ["Statuses"] = new EventFilterViewModel
            {
                Statuses = new List<EventStatus>
                {
                    EventStatus.Blocked,
                    EventStatus.Canceled,
                },
            },
            ["Organizers"] = new EventFilterViewModel
            {
                Organizers = new List<Guid>
                {
                    EventTestData.EventOrganizerId,
                },
            },
            ["Categories"] = new EventFilterViewModel
            {
                Categories = new List<string>
                {
                    EventTestData.EventCategoryId.ToString(),
                },
            },
        };

        public IEnumerator GetEnumerator()
        {
            foreach (var (key, value) in Models)
            {
                yield return new TestCaseData(value)
                {
                    TestName = $"Case_{key}FilterApplied_ReturnsSingleElement",
                };
            }
        }
    }
}
