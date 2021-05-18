using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    public class TrackServiceTests : TestInitializer
    {
        private static Mock<IHttpContextAccessor> _httpContextAccessor;

        private TrackService _service;
        private List<ChangeInfo> _tracks;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            MockMapper = new Mock<IMapper>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpContextAccessor.SetupGet(x => x.HttpContext)
                .Returns(new Mock<HttpContext>().Object);

            _service = new TrackService(Context, MockMapper.Object);

            _tracks = new List<ChangeInfo>
            {
                new ChangeInfo
                {
                    Id = Guid.NewGuid(),
                    EntityName = "Event",
                    ChangesType = ChangesType.Create,
                    Time = DateTime.Today,
                    User = new User { Id = Guid.NewGuid() },
                },
                new ChangeInfo
                {
                    Id = Guid.NewGuid(),
                    EntityName = "EventLocation",
                    ChangesType = ChangesType.Edit,
                    Time = DateTime.Today,
                    User = new User { Id = Guid.NewGuid() },
                },
                new ChangeInfo
                {
                    Id = Guid.NewGuid(),
                    EntityName = "EventCategory",
                    ChangesType = ChangesType.Delete,
                    Time = DateTime.Today,
                    User = new User { Id = Guid.NewGuid() },
                },
                new ChangeInfo
                {
                    Id = Guid.NewGuid(),
                    EntityName = "EventCategory",
                    ChangesType = ChangesType.Undefined,
                    Time = DateTime.Today,
                    User = new User { Id = Guid.NewGuid() },
                },
            };

            Context.ChangeInfos.AddRange(_tracks);
            Context.SaveChanges();

            MockMapper.Setup(u => u.Map<ChangeInfo, TrackDto>(It.IsAny<ChangeInfo>()))
                .Returns((ChangeInfo e) => e == null ? null : new TrackDto { Name = e.EntityName });

            MockMapper.Setup(u => u.Map<TrackDto>(It.IsAny<ChangeInfo>()))
                .Returns((ChangeInfo e) => e == null ? null : new TrackDto { Id = e.Id, Name = e.EntityName });
        }

        [Test]
        public void GetAllTracks_Works()
        {
            TrackFilterViewModel filter = new TrackFilterViewModel()
            {
                Page = 1,
                PageSize = 10,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today,
                EntityName = new List<string>() { "Event", "EventCategory", "EventLocation" },
                ChangesType = new List<ChangesType>
                    { ChangesType.Undefined, ChangesType.Create, ChangesType.Edit, ChangesType.Delete },
            };
            int amountOfTracks = _tracks.Count;

            MockMapper.Setup(u => u.Map<IEnumerable<TrackDto>>(It.IsAny<IEnumerable<ChangeInfo>>()))
                .Returns((IEnumerable<ChangeInfo> e) => e.Select(item => new TrackDto { Id = item.Id }));
            _httpContextAccessor.SetupGet(e => e.HttpContext)
                .Returns(new Mock<HttpContext>().Object);
            var result = _service.GetAllTracks(filter, out var count);
            Assert.AreEqual(amountOfTracks, result.Count());
        }

        [Test]
        public void GetDistinctNames_Works()
        {
            MockMapper.Setup(u => u.Map<IEnumerable<EntityNamesDto>>(It.IsAny<IEnumerable<ChangeInfo>>()))
                .Returns((IEnumerable<ChangeInfo> e) => e?.Select(item => new EntityNamesDto { Id = item.Id }));
            _httpContextAccessor.SetupGet(e => e.HttpContext)
                .Returns(new Mock<HttpContext>().Object);
            var result = _service.GetDistinctNames();
            Assert.AreEqual(3, result.Count());
        }
    }
}
