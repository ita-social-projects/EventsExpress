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
        private static readonly Dictionary<string, EventFilterViewModel> Models = new ()
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
                OrganizerId = Guid.Parse("6e93756a-1920-43b0-a781-0445373f9a7c"),
            },
            ["VisitorId"] = new EventFilterViewModel
            {
                VisitorId = Guid.Parse("2eee6760-db3a-4f0d-8eb9-7c9ccac51092"),
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
                    Guid.Parse("6e93756a-1920-43b0-a781-0445373f9a7c"),
                },
            },
            ["Categories"] = new EventFilterViewModel
            {
                Categories = new List<string>
                {
                    "ef40993e-9118-4380-adc9-4b3910550a59",
                },
            },
        };

        public IEnumerator GetEnumerator()
        {
            foreach (var (key, value) in Models)
            {
                yield return new TestCaseData(value)
                {
                    TestName = $"GetAll_{key}FilterApplied_ReturnsSingleElement",
                };
            }
        }
    }
}
