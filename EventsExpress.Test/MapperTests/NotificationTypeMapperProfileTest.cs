namespace EventsExpress.Test.MapperTests
{
    using EventsExpress.Core.DTOs;
    using EventsExpress.Db.Entities;
    using EventsExpress.Db.Enums;
    using EventsExpress.Mapping;
    using EventsExpress.Test.MapperTests.BaseMapperTestInitializer;
    using EventsExpress.ViewModels;
    using NUnit.Framework;

    [TestFixture]
    internal class NotificationTypeMapperProfileTest : MapperTestInitializer<NotificationTypeMapperProfile>
    {
       private NotificationType GetNotificationType(NotificationChange notificationChange)
        {
            return new NotificationType
            {
                Id = notificationChange,
                Name = notificationChange.ToString(),
            };
        }

       private NotificationTypeDTO GetNotificationTypeDto(NotificationChange notificationChange, int countOfUser)
        {
            return new NotificationTypeDTO
            {
                Id = notificationChange,
                Name = notificationChange.ToString(),
                CountOfUser = countOfUser,
            };
        }

       private NotificationTypeViewModel GetNotificationTypeViewModel(NotificationChange notificationChange, int countOfUser)
        {
            return new NotificationTypeViewModel
            {
                Id = notificationChange,
                Name = notificationChange.ToString(),
                CountOfUser = countOfUser,
            };
        }

       [OneTimeSetUp]
       protected virtual void Init()
        {
            Initialize();
        }

       [Test]
       public void NotificationTypeMapperProfile_Should_HaveValidConfig()
        {
            Configuration.AssertConfigurationIsValid();
        }

       [Test]
       [TestCase(NotificationChange.OwnEvent)]
       [TestCase(NotificationChange.Profile)]
       [TestCase(NotificationChange.VisitedEvent)]
       public void EventMapperProfile_NotificationTypeToNotificationTypeDTO(NotificationChange notificationChange)
        {
            NotificationType notificationType = GetNotificationType(notificationChange);
            var e = Mapper.Map<NotificationType, NotificationTypeDTO>(notificationType);
            Assert.That(e, Is.TypeOf<NotificationTypeDTO>());
            Assert.That(e.Id, Is.EqualTo(notificationType.Id));
            Assert.That(e.Name, Is.EqualTo(notificationType.Name));
            Assert.That(e.CountOfUser, Is.EqualTo(default(int)));
        }

       [Test]
       [TestCase(NotificationChange.OwnEvent, 8)]
       [TestCase(NotificationChange.Profile, 7)]
       [TestCase(NotificationChange.VisitedEvent, 5)]
       public void EventMapperProfile_NotificationTypeDtoToNotificationType(NotificationChange notificationChange, int countOfUser)
        {
            NotificationTypeDTO notificationTypeDTO = GetNotificationTypeDto(notificationChange, countOfUser);
            var e = Mapper.Map<NotificationTypeDTO, NotificationType>(notificationTypeDTO);
            Assert.That(e, Is.TypeOf<NotificationType>());
            Assert.That(e.Id, Is.EqualTo(notificationTypeDTO.Id));
            Assert.That(e.Name, Is.EqualTo(notificationTypeDTO.Name));
            Assert.That(e.Users, Is.Null);
        }

       [Test]
       [TestCase(NotificationChange.OwnEvent, 8)]
       [TestCase(NotificationChange.Profile, 7)]
       [TestCase(NotificationChange.VisitedEvent, 5)]
       public void EventMapperProfile_NotificationTypeDtoToNotificationTypeViewModel(NotificationChange notificationChange, int countOfUser)
        {
            NotificationTypeDTO notificationTypeDTO = GetNotificationTypeDto(notificationChange, countOfUser);
            var e = Mapper.Map<NotificationTypeDTO, NotificationTypeViewModel>(notificationTypeDTO);
            Assert.That(e, Is.TypeOf<NotificationTypeViewModel>());
            Assert.That(e.Id, Is.EqualTo(notificationTypeDTO.Id));
            Assert.That(e.Name, Is.EqualTo(notificationTypeDTO.Name));
            Assert.That(e.CountOfUser, Is.EqualTo(notificationTypeDTO.CountOfUser));
        }

       [Test]
       [TestCase(NotificationChange.OwnEvent, 8)]
       [TestCase(NotificationChange.Profile, 7)]
       [TestCase(NotificationChange.VisitedEvent, 5)]
       public void EventMapperProfile_NotificationTypeViewModelToNotificationTypeDto(NotificationChange notificationChange, int countOfUser)
        {
            NotificationTypeViewModel notificationTypeViewModel = GetNotificationTypeViewModel(notificationChange, countOfUser);
            var e = Mapper.Map<NotificationTypeViewModel, NotificationTypeDTO>(notificationTypeViewModel);
            Assert.That(e, Is.TypeOf<NotificationTypeDTO>());
            Assert.That(e.Id, Is.EqualTo(notificationTypeViewModel.Id));
            Assert.That(e.Name, Is.EqualTo(notificationTypeViewModel.Name));
            Assert.That(e.CountOfUser, Is.EqualTo(notificationTypeViewModel.CountOfUser));
        }
    }
}
