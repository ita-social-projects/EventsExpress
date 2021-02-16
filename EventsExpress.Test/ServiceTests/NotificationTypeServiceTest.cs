using System;
using System.Linq;
using EventsExpress.Core.Services;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class NotificationTypeServiceTest : TestInitializer
    {
        private NotificationTypeService _notificationTypeService;

        [SetUp]
        protected override void Initialize()
        {
            base.Initialize();
            _notificationTypeService = new NotificationTypeService(Context, MockMapper.Object);
        }

        [Test]
        [Category("Get by id")]
        [TestCase(NotificationChange.OwnEvent)]
        [TestCase(NotificationChange.Profile)]
        [TestCase(NotificationChange.VisitedEvent)]
        public void Get_ExistingId_ReturnEntity(NotificationChange id)
        {
            NotificationType assert = _notificationTypeService.GetById(id);
            Assert.That(assert.Id, Is.EqualTo(id));
        }

        [Test]
        [Category("Get by id")]
        [TestCase(-8)]
        [TestCase(-88)]
        [TestCase(-28)]
        public void Get_NotExistingIdORDeletedUnit_Exception(NotificationChange id)
        {
            var res = _notificationTypeService.GetById(id);
            Assert.That(res, Is.Null);
        }

        [Test]
        [Category("Get All")]
        [TestCase(new object[] { NotificationChange.OwnEvent, NotificationChange.Profile, NotificationChange.VisitedEvent })]
        public void Get_ALL_CorrectData(object[] notificationTypes)
        {
            var res = _notificationTypeService.GetAllNotificationTypes();
            Assert.That(res.Where(x => notificationTypes.Contains(x.Id)).Count(), Is.EqualTo(notificationTypes.Length));
        }
    }
}
